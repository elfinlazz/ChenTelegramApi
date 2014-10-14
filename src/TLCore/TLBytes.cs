using System.Collections.Generic;

namespace TelegramApi.TLCore
{
    public class TLBytes
    {
        public TLBytes(byte[] bytes)
        {
            Content = bytes;
        }

        public byte[] Content { get; private set; }
    }
}