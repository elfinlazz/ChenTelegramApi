using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof (UInt32))]
    public class TLUInt32Serializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            return BitConverter.GetBytes((UInt32) input);
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            byte[] arr = byteList.Take(4).ToArray();
            byteList.RemoveRange(0, 4);
            return BitConverter.ToUInt32(arr, 0);
        }
    }
}