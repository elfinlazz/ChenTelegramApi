using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Serialization.Serializer;

namespace TelegramApi.TLCoreTest.Serialization.Serializer
{
    public class TLInt32SerializerTest
    {
        private TLInt32Serializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLInt32Serializer();
        }

        [Test]
        public void Deserialize_ThenInt32Deserialized()
        {
            // arrange
            const int Expected = 0x003FEF02;
            List<byte> byteList = new List<byte>
                {
                    0x02,
                    0xEF,
                    0x3F,
                    0x00
                };

            // act
            Int32 result = (Int32)_testee.Deserialize(byteList, null);

            // assert
            result.ShouldBeEquivalentTo(Expected);
            byteList.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void Serialize_ThenInt32Returned()
        {
            // arrange
            byte[] expected = { 0x02, 0xEF, 0x3F, 0x00 };
            const int Value = 0x003FEF02;

            // act
            List<byte> result = _testee.Serialize(Value, null);

            // assert
            result.ShouldBeEquivalentTo(expected);
        }
    }
}