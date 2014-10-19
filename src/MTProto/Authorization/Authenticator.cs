using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Security;
using TelegramApi.MTProto.Connection;
using TelegramApi.MTProto.Data;
using TelegramApi.TLCore;
using TelegramApi.TLCore.Authorization;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.MTProto.Authorization
{
    public class Authenticator
    {
        private readonly IList<IConnectionInfo> _connectionInfoList;
        private readonly IPqSolver _pqSolver;

        public Authenticator(IList<IConnectionInfo> connectionInfoList, IPqSolver pqSolver)
        {
            _connectionInfoList = connectionInfoList;
            _pqSolver = pqSolver;
        }

        public async Task AttemptAuthentication()
        {
            foreach (IConnectionInfo connectionInfo in _connectionInfoList)
                await DoAuthentication(connectionInfo);
        }

        private async Task DoAuthentication(IConnectionInfo connectionInfo)
        {
            Random r = new Random();
            IPlainConnection connection = new PlainConnection(connectionInfo);
            await connection.ConnectAsync();

            TLReqPqMethod method = new TLReqPqMethod();
            ReqPq reqPq = new ReqPq { Nonce = new byte[16] };
            r.NextBytes(reqPq.Nonce);
            TLFrame<ReqPq> frame = new TLFrame<ReqPq>
                {
                    AuthKey = 0,
                    MessageId = DateTime.Now.ToUnixTime() * (Int64)Math.Pow(2, 32),
                    MessageLength = 20,
                    Content = reqPq
                };

            method.SendObject = frame;

            TLFrame<ResPq> resPqFrame = await connection.ExecuteMethodAsync(method);
            UInt64 pq = (UInt64)TLRootSerializer.Deserialize(resPqFrame.Content.Pq.Content.Reverse().ToList(), typeof(UInt64));
            PqData pqData = _pqSolver.SolvePq(pq);
            PqInnerData innerData = new PqInnerData
                {
                    Nonce = resPqFrame.Content.Nonce,
                    ServerNonce = resPqFrame.Content.ServerNonce,
                    NewNonce = new byte[32],
                    Pq = resPqFrame.Content.Pq,
                    P = new TLBytes(TLRootSerializer.Serialize(pqData.P).Reverse().ToArray()),
                    Q = new TLBytes(TLRootSerializer.Serialize(pqData.Q).Reverse().ToArray())
                };
            r.NextBytes(innerData.NewNonce);

            byte[] data = TLRootSerializer.Serialize(innerData);
            byte[] sha1;

            UInt64 fingerprint = resPqFrame.Content.Vector.Content.First();

            using (SHA1 shaAlgo = SHA1.Create())
            {
                sha1 = shaAlgo.ComputeHash(data);
            }

            List<byte> dataWithHash = sha1.Concat(data).ToList();
            while (dataWithHash.Count < 255)
                dataWithHash.Add((byte)(r.Next() & 0xFF));

            IBufferedCipher cipher = CipherUtilities.GetCipher("RSA/ECB/NoPadding");
            AsymmetricKeyParameter publicKey = ServerKeys.GetKey(fingerprint);
            cipher.Init(true, publicKey);
            byte[] encData = cipher.DoFinal(dataWithHash.ToArray());

            TLFrame<ReqDhParams> reqDhFrame = new TLFrame<ReqDhParams>
                {
                    AuthKey = 0,
                    MessageId = DateTime.Now.ToUnixTime() * (Int64)Math.Pow(2, 32),
                    MessageLength = 320,
                    Content = new ReqDhParams
                        {
                            Nonce = innerData.Nonce,
                            ServerNonce = innerData.ServerNonce,
                            P = innerData.P,
                            Q = innerData.Q,
                            Fingerprint = TLRootSerializer.Serialize(fingerprint),
                            EncryptedData = new TLBytes(encData)
                        }
                };

            TLReqDhParamsMethod reqDhMethod = new TLReqDhParamsMethod { SendObject = reqDhFrame };
            TLFrame<ServerDhParams> res = await connection.ExecuteMethodAsync(reqDhMethod);
        }
    }
}