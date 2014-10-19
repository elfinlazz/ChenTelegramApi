using System;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore
{
    public class TLFrame<T> : TLObject
    {
        [TLProperty(0)]
        public Int64 AuthKey { get; set; }

        [TLProperty(1)]
        public Int64 MessageId { get; set; }

        [TLProperty(2)]
        public Int32 MessageLength { get; set; }

        [TLProperty(3)]
        public T Content { get; set; }
    }
}