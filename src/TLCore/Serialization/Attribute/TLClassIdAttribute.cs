using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TLClassIdAttribute : System.Attribute
    {
        public TLClassIdAttribute(long classId)
        {
            ClassId = classId;
        }

        public long ClassId { get; private set; }
    }
}