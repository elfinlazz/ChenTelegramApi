using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(byte[]))]
    public class TLByteArraySerializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, PropertyInfo propertyInfo)
        {
            return (byte[])input;
        }

        public override object Deserialize(List<byte> byteList, PropertyInfo propertyInfo)
        {
            int arrCount = propertyInfo.GetCustomAttribute<TLPropertyAttribute>().ArrayLength;
            byte[] arr = byteList.Take(arrCount).ToArray();
            byteList.RemoveRange(0, arrCount);
            return arr;
        }
    }
}