using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCore.Serialization
{
    public class TLObjectSerializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, PropertyInfo propertyInfo)
        {
            TLObject obj = (TLObject)input;

            List<byte> list = obj.GetTLProperties()
                .Select(x => TLRootSerializer.Serialize(x.GetValue(obj), propertyInfo))
                .SelectMany(x => x)
                .ToList();

            int classId = obj.GetClassId();
            if (classId != -1)
                list.InsertRange(0, BitConverter.GetBytes(classId));

            return list.ToArray();
        }

        public T Deserialize<T>(List<byte> byteList)
            where T : TLObject, new()
        {
            return (T)Deserialize(byteList, typeof(T));
        }

        public override object Deserialize(List<byte> byteList, PropertyInfo propertyInfo)
        {
            return Deserialize(byteList, propertyInfo.PropertyType);
        }

        public object Deserialize(List<byte> byteList, Type type)
        {
            object obj = Activator.CreateInstance(type);

            int expectedClassId = type.GetClassId();
            if (expectedClassId != -1)
            {
                int classId = BitConverter.ToInt32(byteList.Take(4).ToArray(), 0);
                if (expectedClassId != classId)
                    throw new NotSupportedException(expectedClassId + " =/= " + classId);
                byteList.RemoveRange(0, 4);
            }

            type.GetTLProperties()
                .ToList()
                .ForEach(x => x.SetValue(obj, TLRootSerializer.Deserialize(byteList, x)));

            return obj;
        }
    }
}