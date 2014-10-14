using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCoreTest.Extensions
{
    public class ByteListExtensionsTest
    {
        [Test]
        public void ReadByte_ThenByteRead()
        {
            // arrange
            const byte Expected = 0xEF;
            List<byte> list = new List<byte> { 0xEF, 0xFA };

            // act
            byte result = list.ReadByte();

            // assert
            result.ShouldBeEquivalentTo(Expected);
            list.Count.ShouldBeEquivalentTo(1);
        }
    }
}