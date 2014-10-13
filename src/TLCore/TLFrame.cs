using System;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCore
{
    public class TLFrame : TLObject
    {
        [TLProperty]
        public Int64 AuthKey { get; set; }

        [TLProperty]
        public Int64 MessageId { get; set; }

        [TLProperty]
        public Int32 MessageLength { get; set; }

        [TLProperty]
        public TLObject Content { get; set; }
    }
}