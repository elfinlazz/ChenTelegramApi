using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCore
{
    public abstract class TLObject
    {
        public IEnumerable<PropertyInfo> GetTLProperties()
        {
            return GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(propInfo => propInfo.GetCustomAttribute<TLPropertyAttribute>() != null);
        }

        public int GetClassId()
        {
            return GetType().GetCustomAttribute<TLClassIdAttribute>().ClassId;
        }
    }
}