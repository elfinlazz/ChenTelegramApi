using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCore
{
    public class TLBytes
    {
        public byte[] Content { get; private set; }

        public static TLBytes Deserialize(List<byte> byteList)
        {
            TLBytes bytes = new TLBytes();

            int count = byteList.ReadByte();
            int startOffset = 1;

            if (count > 254)
            {
                count = byteList.ReadByte();
                count += byteList.ReadByte() << 8;
                count += byteList.ReadByte() << 16;
                startOffset = 4;
            }

            bytes.Content = byteList.Take(count).ToArray();
            byteList.RemoveRange(0, count);

            int offset = (count + startOffset) % 4;
            if (offset != 0)
            {
                int offsetCount = 4 - offset;
                for (int i = 0; i < offsetCount; i++)
                    byteList.ReadByte();
            }

            return bytes;
        }
    }
}