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
            // arrange
            const Int64 Expected = 0x100E0C0A08060402;
            List<byte> byteList = new List<byte>
                {
                    0x02,
                    0x04,
                    0x06,
                    0x08,
                    0x0A,
                    0x0C,
                    0x0E,
                    0x10
                };

            // act
            Int64 result = (Int64)_testee.Deserialize(byteList, null);

            // assert
            result.ShouldBeEquivalentTo(Expected);
            byteList.Count.ShouldBeEquivalentTo(0);
        }

        [Test]
        public void Serialize_ThenInt64Returned()
        {
            // arrange
            byte[] expected = { 0x02, 0x04, 0x06, 0x08, 0x0A, 0x0C, 0x0E, 0x10 };
            const Int64 Value = 0x100E0C0A08060402;

            // act
            byte[] result = _testee.Serialize(Value, null);

            // assert
            result.ShouldBeEquivalentTo(expected);
        }
    }
}