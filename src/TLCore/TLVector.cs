using System.Collections.Generic;
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
    }
}