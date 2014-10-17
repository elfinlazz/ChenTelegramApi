using System;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0x05162463)]
    public class ResPq : TLObject
    {
        [TLProperty(ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty]
        public TLBytes Pq { get; set; }

        [TLProperty(VectorType = typeof(Int64))]
        public TLVector<Int64> Vector { get; set; }
    }
}