using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(byte[]))]
    public class TLByteArraySerializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            return (byte[])input;
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            int arrCount = attribute.ArrayLength;
            byte[] arr = byteList.Take(arrCount).ToArray();
            byteList.RemoveRange(0, arrCount);
            return arr;
        }
    }
}