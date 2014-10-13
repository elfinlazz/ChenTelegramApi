using System;
using System.Collections.Generic;
using System.Reflection;

namespace TelegramApi.TLCore.Serialization
{
    public class TLSerializer
    {
        private static void WriteBytes(PropertyInfo propertyInfo, TLObject tlObject, List<byte> byteList)
        {
            if (propertyInfo.PropertyType == typeof(Int64))
                byteList.WriteInt64((Int64)propertyInfo.GetValue(tlObject));

            else if (propertyInfo.PropertyType == typeof(Int32))
                byteList.WriteInt32((Int32)propertyInfo.GetValue(tlObject));

            else if (propertyInfo.PropertyType == typeof(Int16))
                byteList.WriteInt16((Int16)propertyInfo.GetValue(tlObject));

            else if (propertyInfo.PropertyType == typeof(byte[]))
                byteList.WriteByteArray((byte[])propertyInfo.GetValue(tlObject));
        }

        public byte[] Serialize(TLObject tlObject)
        {
            List<byte> byteList = new List<byte>();

            byteList.WriteInt32(tlObject.GetClassId());

            foreach (PropertyInfo propertyInfo in tlObject.GetTLProperties())
                WriteBytes(propertyInfo, tlObject, byteList);

            return byteList.ToArray();
        }

        public T Deserialize<T>(byte[] array) where T : TLObject, new()
        {
            return new T();
        }
    }
}