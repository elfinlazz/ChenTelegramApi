using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore;
using TelegramApi.TLCore.Serialization.Serializer;

namespace TelegramApi.TLCoreTest.Serialization.Serializer
{
    public class TLBytesSerializerTest
    {
        private TLBytesSerializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLBytesSerializer();
        }

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
            TLBytes result = (TLBytes)_testee.Deserialize(byteList, null);

            // assert
            result.Content.ShouldBeEquivalentTo(expected);
            byteList.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void Serialize_ThenBytesReturned()
        {
            // arrange
            byte[] expected = { 0x02, 0xEF, 0x3F, 0x00 };
            TLBytes bytes = new TLBytes(new byte[] { 0xEF, 0x3F });

            // act
            List<byte> result = _testee.Serialize(bytes, null);

            // assert
            result.ShouldBeEquivalentTo(expected);
        }
    }
}