using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0x83c95aec)]
    public class PqInnerData : TLObject
    {
        [TLProperty(0)]
        public TLBytes Pq { get; set; }

        [TLProperty(1)]
        public TLBytes P { get; set; }

        [TLProperty(2)]
        public TLBytes Q { get; set; }

        [TLProperty(3, ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(4, ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty(5, ArrayLength = 32)]
        public byte[] NewNonce { get; set; }
    }
}