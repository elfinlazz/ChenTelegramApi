using System;

namespace TelegramApi.TLCore.Serialization
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TLPropertyAttribute : Attribute
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