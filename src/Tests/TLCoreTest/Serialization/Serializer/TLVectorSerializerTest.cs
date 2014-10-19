using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore;
using TelegramApi.TLCore.Serialization.Attribute;
using TelegramApi.TLCore.Serialization.Serializer;

namespace TelegramApi.TLCoreTest.Serialization.Serializer
{
    public class TLVectorSerializerTest
    {
        private TLVectorSerializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLVectorSerializer();
        }

        [Test]
        public void Deserialize_ThenByteArrayDeserialized()
        {
            // arrange
            byte[] bytes =
                {
                    0x15, 0xC4, 0xB5, 0x1C,
                    0x01, 0x00, 0x00, 0x00,
                    0xFF, 0x7E, 0x3C, 0x88, 0x65, 0xDC, 0xAB, 0x34
                };

            TLVector<Int64> expected = new TLVector<Int64>
                {
                    Content = { 0x34ABDC65883C7EFF }
                };

            // act
            TLVector<Int64> result = (TLVector<Int64>)_testee.Deserialize(bytes.ToList(), new TLPropertyAttribute(0) { VectorType = typeof(Int64) });

            // assert
            result.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void Serialize_ThenByteListReturned()
        {
            // arrange
            TLVector<Int32> vector = new TLVector<Int32>
                {
                    Content = { 0x34ABDC65 }
                };

            byte[] expected =
                {
                    0x15, 0xC4, 0xB5, 0x1C,
                    0x01, 0x00, 0x00, 0x00,
                    0x65, 0xDC, 0xAB, 0x34
                };

            // act
            byte[] result = _testee.Serialize(vector, new TLPropertyAttribute(0) { VectorType = typeof(Int32) });

            // act
            result.ShouldBeEquivalentTo(expected);
        }
    }
}