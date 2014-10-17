using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Kernel;
using TelegramApi.TLCore.Serialization.Attribute;

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

        public static byte[] Serialize(object value, TLPropertyAttribute attribute = null)
        {
            Type type = value.GetType();
            if (type.BaseType == typeof(TLObject))
                type = typeof(TLObject);

            return SerializerDictionary[type].Serialize(value, attribute);
        }

        public static object Deserialize(List<byte> byteList, Type type, TLPropertyAttribute attribute)
        {
            if (type.BaseType == typeof(TLObject))
                return TLObjectSerializer.Deserialize(byteList, type);

            return SerializerDictionary[type].Deserialize(byteList, attribute);
        }

        public static T Deserialize<T>(List<byte> list)
            where T : TLObject, new()
        {
            return (T)Deserialize(list, typeof(T), null);
        }
    }
}