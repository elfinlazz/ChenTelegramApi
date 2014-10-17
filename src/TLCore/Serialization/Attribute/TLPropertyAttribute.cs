using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TLPropertyAttribute : System.Attribute
    {
        public int ArrayLength { get; set; }
        public Type VectorType { get; set; }
    }
}