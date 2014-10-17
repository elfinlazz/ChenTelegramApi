using System;
using System.Collections.Generic;
using System.Reflection;

namespace TelegramApi.TLCore.Serialization
{
    public interface ITLTypeSerializer
    {
        byte[] Serialize(object input, PropertyInfo propertyInfo);

        object Deserialize(List<byte> byteList, PropertyInfo propertyInfo);

        Type GetSerializerType();
    }
}