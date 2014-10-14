using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Kernel;

namespace TelegramApi.TLCore.Serialization
{
    public class TLSerializerFactory : ITLSerializerFactory
    {
        private readonly IDictionary<Type, ITLTypeSerializer> _serializerDictionary;

        public TLSerializerFactory(IEnumerable<ITLTypeSerializer> serializers)
        {
            _serializerDictionary = serializers.ToDictionary(k => k.GetSerializerType(), v => v);
        }

        public ITLTypeSerializer GetSerializer(Type type)
        {
            if (type.BaseType == typeof(TLObject))
                return CoreKernel.Get<ITLObjectDeserializer>(); // TODO: not pretty, please change

            return _serializerDictionary[type];
        }
    }
}