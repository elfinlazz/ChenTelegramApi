using System;

namespace TelegramApi.MTProto.Utils
{
    public static class DebugUtils
    {
        public static void DumpBytes(byte[] bytes)
        {
            Console.WriteLine("Dumping " + bytes.Length + " bytes...");

            int c = bytes[0] == 0xEF ? 14 : 15;

            foreach (byte b in bytes)
            {
                Console.Write("{0:X2} ", b);
                c++;
                if (c == 16)
                {
                    Console.WriteLine(string.Empty);
                    c = 0;
                }
            }
        }
    }
}