using System;
using System.Collections.Generic;
using System.Reflection;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization
{
    public abstract class TLTypeSerializerBase : ITLTypeSerializer
    {
        public abstract List<byte> Serialize(object input, PropertyInfo propertyInfo);

        public abstract object Deserialize(List<byte> byteList, PropertyInfo propertyInfo);

        public Type GetSerializerType()
        {
            return GetType().GetCustomAttribute<TLSerializerAttribute>().Type;
        }
    }
}