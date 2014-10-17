using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(Int64))]
    public class TLInt64Serializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, PropertyInfo propertyInfo)
        {
            return BitConverter.GetBytes((Int64)input);
        }

        public override object Deserialize(List<byte> byteList, PropertyInfo propertyInfo)
        {
            byte[] arr = byteList.Take(8).ToArray();
            byteList.RemoveRange(0, 8);
            return BitConverter.ToInt64(arr, 0);
        }
    }
}