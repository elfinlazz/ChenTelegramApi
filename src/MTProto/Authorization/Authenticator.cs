using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TelegramApi.MTProto.Connection;
using TelegramApi.TLCore;
using TelegramApi.TLCore.Authorization;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Kernel;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.MTProto.Authorization
{
    public class Authenticator
    {
        private readonly IList<IConnectionInfo> _connectionInfoList;

        public Authenticator(IList<IConnectionInfo> connectionInfoList)
        {
            _connectionInfoList = connectionInfoList;
        }

        public async Task AttemptAuthentication()
        {
            foreach (IConnectionInfo connectionInfo in _connectionInfoList)
                await DoAuthentication(connectionInfo);
        }

        private async Task DoAuthentication(IConnectionInfo connectionInfo)
        {
            IPlainConnection connection = new PlainConnection(connectionInfo, CoreKernel.Get<ITLObjectDeserializer>()); // TODO: not pretty, please change
            await connection.ConnectAsync();

            TLReqPqMethod method = new TLReqPqMethod();
            ReqPq reqPq = new ReqPq { Nonce = new byte[16] };
            new Random().NextBytes(reqPq.Nonce);
            TLFrame<ReqPq> frame = new TLFrame<ReqPq>
                {
                    AuthKey = 0,
                    MessageId = DateTime.Now.ToUnixTime() * (Int64)Math.Pow(2, 32),
                    MessageLength = 20,
                    Content = reqPq
                };

            method.SendObject = frame;
            TLFrame<ResPq> result = await connection.ExecuteMethodAsync(method);
        }
    }
}