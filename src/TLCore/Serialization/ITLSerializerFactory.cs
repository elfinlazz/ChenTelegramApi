using System;

namespace TelegramApi.TLCore.Serialization
{
    public interface ITLSerializerFactory
    {
        ITLTypeSerializer GetSerializer(Type type);
    }
}