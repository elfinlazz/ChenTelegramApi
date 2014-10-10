using System;
using System.Collections.Generic;

namespace TelegramApi.TLCore.Serialization
{
    public static class ByteListExtensions
    {
        public static void WriteByteArray(this List<byte> byteList, byte[] arr)
        {
            foreach (byte b in arr)
                byteList.WriteByte(b);
        }

        public static void WriteInt64(this List<byte> byteList, Int64 value)
        {
            byteList.WriteInt32((Int32)((value) & 0xFFFFFFFF));
            byteList.WriteInt32((Int32)((value >> 32) & 0xFFFFFFFF));
        }

        public static void WriteInt32(this List<byte> byteList, Int32 value)
        {
            byteList.WriteInt16((Int16)((value) & 0xFFFF));
            byteList.WriteInt16((Int16)((value >> 16) & 0xFFFF));
        }

        public static void WriteInt16(this List<byte> byteList, Int16 value)
        {
            byteList.WriteByte((byte)((value) & 0xFF));
            byteList.WriteByte((byte)((value >> 8) & 0xFF));
        }

        public static void WriteByte(this List<byte> byteList, byte value)
        {
            byteList.Add(value);
        }
    }
}