using System;

namespace TelegramApi.TLCore.Serialization
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TLClassIdAttribute : Attribute
    {
        public TLClassIdAttribute(int classId)
        {
            ClassId = classId;
        }

        public int ClassId { get; private set; }
    }
}