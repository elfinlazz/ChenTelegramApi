using System;
using System.Collections.Generic;

namespace TelegramApi.TLCore.Extensions
{
    public static class ByteListExtensions
    {
        public static void WriteByteArray(this List<byte> byteList, byte[] arr)
        {
            foreach (byte b in arr)
                byteList.WriteByte(b);
        }

        public static byte[] ReadByteArray(this List<byte> byteList, int count)
        {
            List<byte> bytes = new List<byte>();
            for (int i = 0; i < count; i++)
                bytes.Add(byteList.ReadByte());

            return bytes.ToArray();
        }

        public static void WriteInt64(this List<byte> byteList, Int64 value)
        {
            byteList.WriteInt32((Int32)((value) & 0xFFFFFFFF));
            byteList.WriteInt32((Int32)((value >> 32) & 0xFFFFFFFF));
        }

        public static Int64 ReadInt64(this List<byte> byteList)
        {
            Int32 a = byteList.ReadInt32();
            Int32 b = byteList.ReadInt32();

            return a + (b << 32);
        }

        public static void WriteInt32(this List<byte> byteList, Int32 value)
        {
            byteList.WriteInt16((Int16)((value) & 0xFFFF));
            byteList.WriteInt16((Int16)((value >> 16) & 0xFFFF));
        }

        public static Int32 ReadInt32(this List<byte> byteList)
        {
            Int16 a = byteList.ReadInt16();
            Int16 b = byteList.ReadInt16();

            return a + (b << 16);
        }

        public static void WriteInt16(this List<byte> byteList, Int16 value)
        {
            byteList.WriteByte((byte)((value) & 0xFF));
            byteList.WriteByte((byte)((value >> 8) & 0xFF));
        }

        public static Int16 ReadInt16(this List<byte> byteList)
        {
            byte a = byteList.ReadByte();
            byte b = byteList.ReadByte();

            return (Int16)(a + (b << 8));
        }

        public static void WriteByte(this List<byte> byteList, byte value)
        {
            byteList.Add(value);
        }

        public static byte ReadByte(this List<byte> byteList)
        {
            byte a = byteList[0];
            byteList.RemoveAt(0);
            return a;
        }
    }
}