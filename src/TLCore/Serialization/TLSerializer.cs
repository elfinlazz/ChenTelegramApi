using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCore.Serialization
{
    public class TLSerializer : ITLSerializer
    {
        private void WriteBytes(PropertyInfo propertyInfo, TLObject tlObject, List<byte> byteList)
        {
            if (propertyInfo.PropertyType == typeof(Int64))
                byteList.WriteInt64((Int64)propertyInfo.GetValue(tlObject));

            else if (propertyInfo.PropertyType == typeof(Int32))
                byteList.WriteInt32((Int32)propertyInfo.GetValue(tlObject));

            else if (propertyInfo.PropertyType == typeof(byte[]))
                byteList.WriteByteArray((byte[])propertyInfo.GetValue(tlObject));

            else if (propertyInfo.PropertyType.BaseType == typeof(TLObject))
                byteList.WriteByteArray(Serialize((TLObject)propertyInfo.GetValue(tlObject)));
        }

        private void ReadBytes(PropertyInfo propertyInfo, TLObject tlObject, List<byte> byteList)
        {
            if (propertyInfo.PropertyType == typeof(Int64))
                propertyInfo.SetValue(tlObject, byteList.ReadInt64());

            else if (propertyInfo.PropertyType == typeof(Int32))
                propertyInfo.SetValue(tlObject, byteList.ReadInt32());

            else if (propertyInfo.PropertyType == typeof(byte[]))
                propertyInfo.SetValue(tlObject, byteList.ReadByteArray(propertyInfo.GetCustomAttribute<TLPropertyAttribute>().ArrayLength));

            else if (propertyInfo.PropertyType.BaseType == typeof(TLObject))
                propertyInfo.SetValue(tlObject, DeserializeObject(propertyInfo.PropertyType, byteList.ToArray()));

            else if (propertyInfo.PropertyType == typeof(TLBytes))
                propertyInfo.SetValue(tlObject, TLBytes.Deserialize(byteList));

            else if (propertyInfo.PropertyType == typeof(TLVector<>))
                propertyInfo.SetValue(tlObject, null);
        }

        public byte[] Serialize(TLObject tlObject)
        {
            List<byte> byteList = new List<byte>();

            int classId = tlObject.GetClassId();
            if (classId != -1)
                byteList.WriteInt32(classId);

            foreach (PropertyInfo propertyInfo in tlObject.GetTLProperties())
                WriteBytes(propertyInfo, tlObject, byteList);

            return byteList.ToArray();
        }

        public T Deserialize<T>(byte[] array)
            where T : TLObject, new()
        {
            T result = new T();
            List<byte> byteList = array.ToList();

            foreach (PropertyInfo propertyInfo in result.GetTLProperties())
                ReadBytes(propertyInfo, result, byteList);

            return result;
        }

        public TLObject DeserializeObject(Type type, byte[] array)
        {
            TLObject result = (TLObject)Activator.CreateInstance(type);

            List<byte> byteList = array.ToList();

            int classId = byteList.ReadInt32();
            if (classId != type.GetClassId())
                throw new NotSupportedException("Invalid type");

            foreach (PropertyInfo propertyInfo in type.GetTLProperties())
                ReadBytes(propertyInfo, result, byteList);

            return result;
        }
    }
}