using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCoreTest.Extensions
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
        
        [Test]
        public void ReadInt32_ThenInt16Read()
        {
            // arrange
            const Int32 Expected = 0x1FFA1357;
            List<byte> list = new List<byte> { 0x57, 0x13, 0xFA, 0x1F, 0x00 };

            // act
            Int32 result = list.ReadInt32();

            // assert
            result.ShouldBeEquivalentTo(Expected);
            list.Count.ShouldBeEquivalentTo(1);
        }

        [Test]
        public void ReadInt64_ThenInt16Read()
        {
            // arrange
            const Int64 Expected = 0x3FFA135775312211;
            List<byte> list = new List<byte> { 0x11, 0x22, 0x31, 0x75, 0x57, 0x13, 0xFA, 0x3F, 0x00 };

            // act
            Int64 result = list.ReadInt64();

            // assert
            result.ShouldBeEquivalentTo(Expected);
            list.Count.ShouldBeEquivalentTo(1);
        }

        [Test]
        public void ReadByteArray_ThenByteArrayRead()
        {
            // arrange
            byte[] expected = { 0x11, 0x22, 0x31, 0x75, 0x57, 0x13, 0xFA, 0x3F };
            List<byte> list = new List<byte> { 0x11, 0x22, 0x31, 0x75, 0x57, 0x13, 0xFA, 0x3F, 0x00 };

            // act
            byte[] result = list.ReadByteArray(8);

            // assert
            result.ShouldBeEquivalentTo(expected);
            list.Count.ShouldBeEquivalentTo(1);
        }
    }
}