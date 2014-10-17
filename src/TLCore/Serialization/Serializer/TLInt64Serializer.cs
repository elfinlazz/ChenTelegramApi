using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(Int64))]
    public class TLInt64Serializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            return BitConverter.GetBytes((Int64)input);
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            byte[] arr = byteList.Take(8).ToArray();
            byteList.RemoveRange(0, 8);
            return BitConverter.ToInt64(arr, 0);
        }
    }
}