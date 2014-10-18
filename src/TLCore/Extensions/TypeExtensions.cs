using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Extensions
{
    public static class TypeExtensions
    {
        public static IEnumerable<PropertyInfo> GetTLProperties(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(propInfo => propInfo.GetCustomAttribute<TLPropertyAttribute>() != null);
        }

        public static UInt32 GetClassId(this Type type)
        {
            TLClassIdAttribute attr = type.GetCustomAttribute<TLClassIdAttribute>();
            return attr == null ? 0 : attr.ClassId;
        }
    }
}