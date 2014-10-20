using System;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0xb5890dba)]
    public class ServerDhInnerData : TLObject
    {
        [TLProperty(0, ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(1, ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty(2)]
        public Int32 G { get; set; }

        [TLProperty(3)]
        public TLBytes DhPrime { get; set; }

        [TLProperty(4)]
        public TLBytes Ga { get; set; }

        [TLProperty(5)]
        public Int32 ServerTime { get; set; }
    }
}