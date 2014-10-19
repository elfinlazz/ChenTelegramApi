using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Serialization.Attribute;
using TelegramApi.TLCore.Serialization.Serializer;

namespace TelegramApi.TLCoreTest.Serialization.Serializer
{
    public class TLByteArraySerializerTest
    {
        private TLByteArraySerializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLByteArraySerializer();
        }

        [Test]
        public void Deserialize_ThenByteArrayDeserialized()
        {
            // arrange
            byte[] expected =
                {
                    0x02, 0x4F, 0x55, 0x43
                };
            List<byte> byteList = new List<byte>
                {
                    0x02,
                    0x4F,
                    0x55,
                    0x43,
                    0x65,
                    0x44
                };

            // act
            byte[] result = (byte[])_testee.Deserialize(byteList, new TLPropertyAttribute(0) { ArrayLength = 4 });

            // assert
            result.ShouldBeEquivalentTo(expected);
            byteList.Count.ShouldBeEquivalentTo(2);
        }

        [Test]
        public void Serialize_ThenByteListReturned()
        {
            // arrange
            List<byte> expected = new List<byte>
                {
                    0x02,
                    0x4F,
                    0x55,
                    0x43,
                    0x65,
                    0x44
                };
            byte[] byteArr =
                {
                    0x02, 0x4F, 0x55, 0x43, 0x65, 0x44
                };

            // act
            byte[] result = _testee.Serialize(byteArr, null);

            // assert
            result.ShouldBeEquivalentTo(expected);
        }
    }
}