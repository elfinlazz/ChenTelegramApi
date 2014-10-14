using System;
using System.Linq;
using System.Reflection;
using Ninject.Infrastructure.Language;
using Ninject.Modules;
using TelegramApi.TLCore.Serialization;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Kernel
{
    public class SerializerModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ITLObjectDeserializer>().To<TLObjectSerializer>().InSingletonScope();
            Bind<ITLSerializerFactory>().To<TLSerializerFactory>().InSingletonScope();

            foreach (Type serializerType in Assembly.GetAssembly(typeof(TLSerializerFactory))
                .GetTypes()
                .Where(x => x.HasAttribute<TLSerializerAttribute>()))
            {
                Bind<ITLTypeSerializer>().To(serializerType);
            }
        }
    }
}