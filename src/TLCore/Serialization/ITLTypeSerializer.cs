using System;
using System.Collections.Generic;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization
{
    public interface ITLTypeSerializer
    {
        byte[] Serialize(object input, TLPropertyAttribute attribute);

        object Deserialize(List<byte> byteList, TLPropertyAttribute attribute);

        Type GetSerializerType();
    }
}