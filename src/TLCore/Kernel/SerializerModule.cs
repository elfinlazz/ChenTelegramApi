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
            foreach (Type serializerType in Assembly.GetAssembly(typeof(TLRootSerializer))
                .GetTypes()
                .Where(x => x.HasAttribute<TLSerializerAttribute>()))
            {
                Bind<ITLTypeSerializer>().To(serializerType);
            }
        }
    }
}