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
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(propInfo => propInfo.GetCustomAttribute<TLPropertyAttribute>() != null)
                .OrderBy(propInfo => propInfo.GetCustomAttribute<TLPropertyAttribute>().Order);
        }

        public static UInt32 GetClassId(this Type type)
        {
            TLClassIdAttribute attr = type.GetCustomAttribute<TLClassIdAttribute>();
            return attr == null ? 0 : attr.ClassId;
        }

        public static Type GetSubTypeWithClassId(this Type type, UInt32 classId)
        {
            return type.Assembly.GetTypes().Where(x => x.IsSubclassOf(type)).Single(x => x.GetClassId() == classId);
        }
    }
}