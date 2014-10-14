using System;
using TelegramApi.TLCore.Serialization.Attribute;

namespace TelegramApi.TLCore
{
    public class TLFrame<T> : TLObject
    {
        [TLProperty]
        public Int64 AuthKey { get; set; }

        [TLProperty]
        public Int64 MessageId { get; set; }

        [TLProperty]
        public Int32 MessageLength { get; set; }

        [TLProperty]
        public T Content { get; set; }
    }
}