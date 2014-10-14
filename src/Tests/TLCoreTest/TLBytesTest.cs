using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore;

namespace TelegramApi.TLCoreTest
{
    public class TLBytesTest
    {
        [Test]
        public void Deserialize_ThenBytesDeserialized()
        {
            // arrange
            byte[] expected = { 0xEF, 0x3F };
            List<byte> byteList = new List<byte>
                {
                    0x02,
                    0xEF,
                    0x3F,
                    0x00
                };

            // act
            TLBytes result = TLBytes.Deserialize(byteList);

            // assert
            result.Content.ShouldBeEquivalentTo(expected);
            byteList.Count.ShouldBeEquivalentTo(0);
        }
    }
}