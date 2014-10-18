using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TLClassIdAttribute : System.Attribute
    {
        public TLClassIdAttribute(UInt32 classId)
        {
            ClassId = classId;
        }

        public UInt32 ClassId { get; private set; }
    }
}