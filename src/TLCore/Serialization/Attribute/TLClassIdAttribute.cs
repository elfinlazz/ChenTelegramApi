using System;

namespace TelegramApi.TLCore.Serialization.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class TLClassIdAttribute : System.Attribute
    {
        public TLClassIdAttribute(int classId)
        {
            ClassId = classId;
        }

        public int ClassId { get; private set; }
    }
}