using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TLClassIdAttribute : System.Attribute
    {
        public TLClassIdAttribute(uint classId)
        {
            ClassId = classId;
        }

        public uint ClassId { get; private set; }
    }
}