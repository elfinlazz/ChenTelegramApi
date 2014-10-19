using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(UInt64))]
    public class TLUInt64Serializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            return BitConverter.GetBytes((UInt64)input);
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            byte[] arr = byteList.Take(8).ToArray();
            byteList.RemoveRange(0, 8);
            return BitConverter.ToUInt64(arr, 0);
        }
    }
}