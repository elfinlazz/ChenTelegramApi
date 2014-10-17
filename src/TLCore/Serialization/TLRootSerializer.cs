using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Kernel;

namespace TelegramApi.TLCore.Serialization
{
    public static class TLRootSerializer
    {
        private static readonly IDictionary<Type, ITLTypeSerializer> SerializerDictionary;

        static TLRootSerializer()
        {
            SerializerDictionary = CoreKernel.GetAll<ITLTypeSerializer>().ToDictionary(k => k.GetSerializerType(), v => v);
            SerializerDictionary.Add(typeof(TLObject), new TLObjectSerializer());
        }

        public static byte[] Serialize(object value, PropertyInfo propertyInfo = null)
        {
            Type type = value.GetType();
            if (type.BaseType == typeof(TLObject))
                type = typeof(TLObject);

            return SerializerDictionary[type].Serialize(value, propertyInfo);
        }

        public static object Deserialize(List<byte> byteList, PropertyInfo propertyInfo)
        {
            Type type = propertyInfo.PropertyType;
            if (type.BaseType == typeof(TLObject))
                type = typeof(TLObject);

            return SerializerDictionary[type].Deserialize(byteList, propertyInfo);
        }

        public static object Deserialize(List<byte> byteList, Type type)
        {
            return SerializerDictionary[type].Deserialize(byteList, null);
        }

        public static T Deserialize<T>(List<byte> list)
            where T : TLObject, new()
        {
            return (T)Deserialize(list, typeof(T));
        }
    }
}