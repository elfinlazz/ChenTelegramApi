using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization
{
    public class TLObjectSerializer : TLTypeSerializerBase
    {
        public static T Deserialize<T>(List<byte> byteList)
            where T : TLObject, new()
        {
            return (T)Deserialize(byteList, typeof(T));
        }

        public static object Deserialize(List<byte> byteList, Type type)
        {
            UInt32 expectedClassId = type.GetClassId();
            if (expectedClassId == 0xFFFFFFFF)
            {
                UInt32 classId = (UInt32)TLRootSerializer.Deserialize(byteList, typeof(UInt32));
                type = type.GetSubTypeWithClassId(classId);
            }
            else if (expectedClassId != 0)
            {
                UInt32 classId = (UInt32)TLRootSerializer.Deserialize(byteList, typeof(UInt32));
                if (expectedClassId != classId)
                    throw new NotSupportedException(expectedClassId + " =/= " + classId);
            }

            object obj = Activator.CreateInstance(type);

            type.GetTLProperties()
                .ToList()
                .ForEach(x => x.SetValue(obj, TLRootSerializer.Deserialize(byteList, x.PropertyType, x.GetCustomAttribute<TLPropertyAttribute>())));

            return obj;
        }

        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            TLObject obj = (TLObject)input;

            List<byte> list = obj.GetTLProperties()
                .Select(x => TLRootSerializer.Serialize(x.GetValue(obj), attribute))
                .SelectMany(x => x)
                .ToList();

            UInt32 classId = obj.GetClassId();
            if (classId != 0)
                list.InsertRange(0, TLRootSerializer.Serialize(classId));

            return list.ToArray();
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            throw new NotSupportedException("Use static Deserialize<T>(List<byte>) or Deserialize(List<byte>, Type)");
        }
    }
}