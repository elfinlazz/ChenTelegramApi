using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(Int32))]
    public class TLInt32Serializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, PropertyInfo propertyInfo)
        {
            return BitConverter.GetBytes((Int32)input);
        }

        public override object Deserialize(List<byte> byteList, PropertyInfo propertyInfo)
        {
            byte[] arr = byteList.Take(4).ToArray();
            byteList.RemoveRange(0, 4);
            return BitConverter.ToInt32(arr, 0);
        }
    }
}