using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Serialization.Serializer;

namespace TelegramApi.TLCoreTest.Serialization.Serializer
{
    public class TLInt64SerializerTest
    {

        private TLInt64Serializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLInt64Serializer();
        }

        [Test]
        public void Deserialize_ThenInt64Deserialize()
        {
            //arrange
            const Int64 Expected = 0x00AB013FEF02;
            List<byte> byteList = new List<byte>
            {
                0x02,
                0xEF,
                0x3F,
                0x01,
                0xAB,
                0x00,
                0x00,
                0x00
            };

            //act
            Int64 result = (Int64) _testee.Deserialize(byteList, null);

            //assert
            result.ShouldBeEquivalentTo(Expected);
            byteList.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void Serialize_ThenInt64Returned()
        {
            //arrange
            byte[] expected = { 0x02, 0xEF, 0x3F, 0x01, 0xAB, 0x00, 0x00, 0x00 };
            const Int64 Value = 0x00AB013FEF02;

            //act
            List<byte> result = _testee.Serialize(Value, null);

            //assert
            result.ShouldBeEquivalentTo(expected);
        }

    }
}
