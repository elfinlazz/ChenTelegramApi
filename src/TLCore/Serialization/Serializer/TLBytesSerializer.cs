using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore.Serialization.Serializer
{
    [TLSerializer(typeof(TLBytes))]
    public class TLBytesSerializer : TLTypeSerializerBase
    {
        public override byte[] Serialize(object input, TLPropertyAttribute attribute)
        {
            TLBytes bytes = (TLBytes)input;
            int len = bytes.Content.Length;
            int startOffset = 1;

            List<byte> list = new List<byte>();

            if (len >= 254)
            {
                list.Add(254);
                list.Add((byte)(len & 0xFF));
                list.Add((byte)((len >> 8) & 0xFF));
                list.Add((byte)((len >> 16) & 0xFF));
                startOffset = 4;
            }
            else
            {
                list.Add((byte)len);
            }

            list.AddRange(bytes.Content);

            int offset = (len + startOffset) % 4;
            if (offset != 0)
            {
                int offsetCount = 4 - offset;
                list.AddRange(Enumerable.Repeat((byte)0x00, offsetCount));
            }

            return list.ToArray();
        }

        public override object Deserialize(List<byte> byteList, TLPropertyAttribute attribute)
        {
            int count = byteList.ReadByte();
            int startOffset = 1;

            if (count >= 254)
            {
                count = byteList.ReadByte();
                count += byteList.ReadByte() << 8;
                count += byteList.ReadByte() << 16;
                startOffset = 4;
            }

            TLBytes bytes = new TLBytes(byteList.Take(count).ToArray());
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