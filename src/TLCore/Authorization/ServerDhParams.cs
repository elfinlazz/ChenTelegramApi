using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0xFFFFFFFF)]
    public class ServerDhParams : TLObject
    {
        [TLProperty(0, ArrayLength = 16)]
        public byte[] Nonce { get; set; }

        [TLProperty(1, ArrayLength = 16)]
        public byte[] ServerNonce { get; set; }
    }

    [TLClassId(0xd0e8075c)]
    public class ServerDhParamsOk : ServerDhParams
    {
        [TLProperty(2)]
        public TLBytes EncryptedAnswer { get; set; }
    }

    [TLClassId(0x79cb045d)]
    public class ServerDhParamsFail : ServerDhParams
    {
        [TLProperty(2, ArrayLength = 16)]
        public byte[] NewNonce { get; set; }
    }
}