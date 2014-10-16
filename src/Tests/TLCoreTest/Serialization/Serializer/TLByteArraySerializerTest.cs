using System.Collections.Generic;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Serialization.Attribute;
using TelegramApi.TLCore.Serialization.Serializer;

namespace TelegramApi.TLCoreTest.Serialization.Serializer
{
    public class TLByteArraySerializerTest
    {
        private TLByteArraySerializer _testee;

        [TLProperty(4)]
        public byte[] TestProperty { get; set; }

        [SetUp]
        public void SetUp()
        {
            _testee = new TLByteArraySerializer();
        }

        [Test]
        public void Deserialize_ThenInt32Deserialized()
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
            byte[] result = (byte[])_testee.Deserialize(byteList, GetType().GetProperty("TestProperty", BindingFlags.Public | BindingFlags.Instance));

            // assert
            result.ShouldBeEquivalentTo(expected);
            byteList.Count.ShouldBeEquivalentTo(2);
        }

        [Test]
        public void Serialize_ThenInt32Returned()
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
            List<byte> result = _testee.Serialize(byteArr, null);

            // assert
            result.ShouldBeEquivalentTo(expected);
        }
    }
}