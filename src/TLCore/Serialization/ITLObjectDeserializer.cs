using System.Collections.Generic;

namespace TelegramApi.TLCore.Serialization
{
    public interface ITLObjectDeserializer : ITLTypeSerializer
    {
        T Deserialize<T>(List<byte> byteList)
            where T : TLObject, new();
    }
}