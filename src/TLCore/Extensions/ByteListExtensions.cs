using System.Collections.Generic;

namespace TelegramApi.TLCore.Extensions
{
    public static class ByteListExtensions
    {
        public static byte ReadByte(this List<byte> byteList)
        {
            byte a = byteList[0];
            byteList.RemoveAt(0);
            return a;
        }
    }
}