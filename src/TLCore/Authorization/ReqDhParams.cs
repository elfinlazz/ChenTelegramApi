using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0xd712e4be)]
    public class ReqDhParams : TLObject
    {
        [TLProperty(0, ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(1, ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }

        [TLProperty(2)]
        public TLBytes P { get; set; }

        [TLProperty(3)]
        public TLBytes Q { get; set; }

        [TLProperty(4, ArrayLength = 8)]
        public byte[] Fingerprint { get; set; }

        [TLProperty(5)]
        public TLBytes EncryptedData { get; set; }
    }
}