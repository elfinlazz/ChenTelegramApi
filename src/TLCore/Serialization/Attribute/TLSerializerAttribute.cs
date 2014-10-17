using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TLSerializerAttribute : System.Attribute
    {
        public TLSerializerAttribute(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}