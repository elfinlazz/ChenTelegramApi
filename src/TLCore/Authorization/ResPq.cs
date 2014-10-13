using System;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0x05162463)]
    public class ResPq : TLObject
    {
        [TLProperty(16)]
        public byte[] Nonce { get; set; }

        [TLProperty(16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty]
        public TLBytes Pq { get; set; }

        [TLProperty]
        public TLVector<Int64> Vector { get; set; }
    }
}