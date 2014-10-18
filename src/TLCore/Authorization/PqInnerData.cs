using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0x83c95aec)]
    public class PqInnerData : TLObject
    {
        [TLProperty]
        public byte[] Pq { get; set; }

        [TLProperty]
        public byte[] P { get; set; }

        [TLProperty(ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty(ArrayLength = 32)]
        public byte[] NewNonce { get; set; }
    }
}