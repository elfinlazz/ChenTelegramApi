using TelegramApi.TLCore.Serialization;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Authorization
{
    [TLClassId(0x60469778)]
    public class ReqPq : TLObject
    {
        [TLProperty(16)]
        public byte[] Nonce { get; set; }
    }
}