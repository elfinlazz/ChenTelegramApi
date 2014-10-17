using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(TLVector<>))]
    public class TLVectorSerializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            dynamic vector = input;

            int classId = GetSerializerType().GetClassId();
            int len = vector.Content.Count;

            List<byte> byteList = new List<byte>();
            byteList.AddRange(TLRootSerializer.Serialize(classId));
            byteList.AddRange(TLRootSerializer.Serialize(len));

            foreach (dynamic d in vector.Content)
                byteList.AddRange(TLRootSerializer.Serialize(d));

            return byteList.ToArray();
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            int expectedClassId = GetSerializerType().GetClassId();
            int classId = BitConverter.ToInt32(byteList.Take(4).ToArray(), 0);
            if (expectedClassId != classId)
                throw new NotSupportedException(expectedClassId + " =/= " + classId);
            byteList.RemoveRange(0, 4);

            dynamic vector = Activator.CreateInstance(typeof(TLVector<>).MakeGenericType(attribute.VectorType));

            int len = BitConverter.ToInt32(byteList.Take(4).ToArray(), 0);
            byteList.RemoveRange(0, 4);

            for (int i = 0; i < len; i++)
                vector.Content.Add((dynamic)TLRootSerializer.Deserialize(byteList, attribute.VectorType, null));

            return vector;
        }
    }
}