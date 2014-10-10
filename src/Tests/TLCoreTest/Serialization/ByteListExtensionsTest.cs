using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCoreTest.Serialization
{
    public class ByteListExtensionsTest
    {
        [Test]
        public void WriteByte_WhenByteWritten_ThenByteAdded()
        {
            // arrange
            byte[] expectedBytes = { 0x1A };
            List<byte> list = new List<byte>();

            // act
            list.WriteByte(0x1A);

            // assert
            list.Should().ContainInOrder(expectedBytes);
        }

        [Test]
        public void WriteInt16_WhenIntWritten_ThenBytesAdded()
        {
            // arrange
            byte[] expectedBytes = { 0x1A, 0x1F };
            List<byte> list = new List<byte>();

            // act
            list.WriteInt16(0x1F1A);

            // assert
            list.Should().ContainInOrder(expectedBytes);
        }

        [Test]
        public void WriteInt32_WhenIntWritten_ThenBytesAdded()
        {
            // arrange
            byte[] expectedBytes = { 0x78, 0x97, 0x46, 0x60 };
            List<byte> list = new List<byte>();

            // act
            list.WriteInt32(0x60469778);

            // assert
            list.Should().ContainInOrder(expectedBytes);
        }

        [Test]
        public void WriteInt64_WhenIntWritten_ThenBytesAdded()
        {
            // arrange
            byte[] expectedBytes = { 0x4A, 0x96, 0x70, 0x27, 0xC4, 0x7A, 0xE5, 0x51 };
            List<byte> list = new List<byte>();

            // act
            list.WriteInt64(0x51e57ac42770964a);

            // assert
            list.Should().ContainInOrder(expectedBytes);
        }

        [Test]
        public void WriteByteArray_ThenBytesWritten()
        {
            // arrange
            byte[] expectedBytes = { 0x4A, 0x96, 0x70, 0x27, 0xC4, 0x7A, 0xE5, 0x51 };
            List<byte> list = new List<byte>();

            // act
            list.WriteByteArray(new byte[] { 0x4A, 0x96, 0x70, 0x27, 0xC4, 0x7A, 0xE5, 0x51 });

            // assert
            list.Should().ContainInOrder(expectedBytes);
        }
    }
}