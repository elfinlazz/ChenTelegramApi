using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Authorization;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCoreTest.Serialization
{
    public class TLSerializerTest
    {
        private TLSerializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLSerializer();
        }

        [Test]
        public void Serialize_WhenObjectSerialized_ThenExpectedBytesReturned()
        {
            // arrange
            ReqPq reqPq = new ReqPq
                {
                    Nonce = new byte[]
                        {
                            0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                            0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                        }
                };

            byte[] expectedBytes =
            {
                0x78, 0x97, 0x46, 0x60,
                0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
            };

            // act
            byte[] result = _testee.Serialize(reqPq);

            // assert
            result.ShouldBeEquivalentTo(expectedBytes);
        }
    }
}