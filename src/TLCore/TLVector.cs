using System.Collections.Generic;
using TelegramApi.TLCore.Extensions;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCore
{
    [TLClassId(0x1cb5c415)]
    public class TLVector<T>
    {
        public TLVector()
        {
            Content = new List<T>();
        }

        public List<T> Content { get; private set; }

        public static TLVector<T> Deserialize(List<byte> byteList)
        {
            TLVector<T> vector = new TLVector<T>();

            int len = byteList.ReadInt32();
            for (int i = 0; i < len; i++)
            {
            }

            return vector;
        }
    }
}