using System;
using System.Collections.Generic;
using System.Reflection;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization
{
    public abstract class TLTypeSerializerBase : ITLTypeSerializer
    {
        public abstract byte[] Serialize(object input, TLPropertyAttribute attribute);

        public abstract object Deserialize(List<byte> byteList, TLPropertyAttribute attribute);

        public Type GetSerializerType()
        {
            return GetType().GetCustomAttribute<TLSerializerAttribute>().Type;
        }
    }
}