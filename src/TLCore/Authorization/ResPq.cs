using System;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0x05162463)]
    public class ResPq : TLObject
    {
        [TLProperty(0, ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(1, ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty(2)]
        public TLBytes Pq { get; set; }

        [TLProperty(3, VectorType = typeof(UInt64))]
        public TLVector<UInt64> Vector { get; set; }
    }
}