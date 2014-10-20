using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TelegramApi.MTProto.Connection;
using TelegramApi.MTProto.Encryption;
using TelegramApi.MTProto.Utils;
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
        private readonly IRsaCrypter _rsaCrypter;

        public Authenticator(
            IList<IConnectionInfo> connectionInfoList,
            IPqSolver pqSolver,
            IRsaCrypter rsaCrypter)
        {
            _connectionInfoList = connectionInfoList;
            _pqSolver = pqSolver;
            _rsaCrypter = rsaCrypter;
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

            UInt64 fingerprint = resPqFrame.Content.Vector.Content.First();
            SHA1 shaAlgo = SHA1.Create();
            byte[] sha1 = shaAlgo.ComputeHash(data);

            List<byte> dataWithHash = sha1.Concat(data).ToList();
            while (dataWithHash.Count < 255)
                dataWithHash.Add((byte)(r.Next() & 0xFF));

            byte[] encData = _rsaCrypter.EncryptBytes(dataWithHash.ToArray(), fingerprint);

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
            TLFrame<ServerDhParams> dhParamsFrame = await connection.ExecuteMethodAsync(reqDhMethod);

            ServerDhParamsOk paramsOk = dhParamsFrame.Content as ServerDhParamsOk;
            if (paramsOk == null)
                throw new ServerException("Didn't receive ServerDhParamsOk(0xd0e8075c)");

            // SHA1(new_nonce + server_nonce)
            byte[] tmpAesKey = shaAlgo.ComputeHash(ArrayUtils.Concat(innerData.NewNonce, innerData.ServerNonce));
            // + substr (SHA1(server_nonce + new_nonce), 0, 12)
            tmpAesKey = tmpAesKey.Concat(shaAlgo.ComputeHash(ArrayUtils.Concat(innerData.ServerNonce, innerData.NewNonce)).Take(12).ToArray()).ToArray();

            // substr (SHA1(server_nonce + new_nonce), 12, 8)
            byte[] tmpAesIv = shaAlgo.ComputeHash(ArrayUtils.Concat(innerData.ServerNonce, innerData.NewNonce)).Skip(12).Take(8).ToArray();
            // + SHA1(new_nonce + new_nonce)
            tmpAesIv = tmpAesIv.Concat(shaAlgo.ComputeHash(ArrayUtils.Concat(innerData.NewNonce, innerData.NewNonce))).ToArray();
            // + substr (new_nonce, 0, 4);
            tmpAesIv = tmpAesIv.Concat(innerData.NewNonce.Take(4)).ToArray();

            byte[] decData = _rsaCrypter.DecryptBytes(paramsOk.EncryptedAnswer.Content, tmpAesKey, tmpAesIv);
            byte[] hash = decData.Take(20).ToArray();
            decData = decData.Skip(20).ToArray();

            ServerDhInnerData dhInnerData = TLRootSerializer.Deserialize<ServerDhInnerData>(decData.ToList());

            if (!ArrayUtils.Equal(hash, shaAlgo.ComputeHash(TLRootSerializer.Serialize(dhInnerData))))
                throw new SecurityException("SHA-1 hash not equal to ServerDhInnerData");
        }
    }
}