using System;
using System.Collections.Generic;
using System.Linq;
using TelegramApi.TLCore.Serialization;
using TelegramApi.TLCore.Serialization.Attribute;

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

            int len = BitConverter.ToInt32(byteList.Take(4).ToArray(), 0);
            byteList.RemoveRange(0, 4);
            for (int i = 0; i < len; i++)
            {
            }

            return vector;
        }
    }
}