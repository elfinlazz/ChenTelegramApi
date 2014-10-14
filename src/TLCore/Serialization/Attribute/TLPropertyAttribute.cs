using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TLPropertyAttribute : System.Attribute
    {
        public TLPropertyAttribute()
        {
        }

        public TLPropertyAttribute(int arrayLength)
        {
            ArrayLength = arrayLength;
        }

        public int ArrayLength { get; set; }
    }
}