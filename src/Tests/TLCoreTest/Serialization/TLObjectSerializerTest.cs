using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore;
using TelegramApi.TLCore.Authorization;
using TelegramApi.TLCore.Serialization;

namespace TelegramApi.TLCoreTest.Serialization
{
    public class TLObjectSerializerTest
    {
        private TLObjectSerializer _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new TLObjectSerializer();
        }

        [Test]
        public void Serialize_WhenFrameReqPqSerialized_ThenExpectedBytesReturned()
        {
            // arrange
            TLFrame<ReqPq> reqPq = new TLFrame<ReqPq>
                {
                    AuthKey = 0,
                    MessageId = 0,
                    MessageLength = 20,
                    Content = new ReqPq
                        {
                            Nonce = new byte[]
                                {
                                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                                    0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                                }
                        }
                };

            byte[] expectedBytes =
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x14, 0x00, 0x00, 0x00,
                    0x78, 0x97, 0x46, 0x60,
                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                };

            // act
            byte[] result = _testee.Serialize(reqPq, null).ToArray();

            // assert
            result.ShouldBeEquivalentTo(expectedBytes);
        }

        [Test]
        public void Deserialize_WhenFrameReqPqDeserialized_ThenExpectedObjectReturned()
        {
            // arrange
            byte[] bytes =
                {
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                    0x14, 0x00, 0x00, 0x00,
                    0x78, 0x97, 0x46, 0x60,
                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                    0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                };

            TLFrame<ReqPq> expectedFrame = new TLFrame<ReqPq>
                {
                    AuthKey = 0,
                    MessageId = 0,
                    MessageLength = 20,
                    Content = new ReqPq
                        {
                            Nonce = new byte[]
                                {
                                    0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,
                                    0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F, 0x10
                                }
                        }
                };

            // act
            TLFrame<ReqPq> result = TLObjectSerializer.Deserialize<TLFrame<ReqPq>>(bytes.ToList());

            // assert
            result.ShouldBeEquivalentTo(expectedFrame);
        }
    }
}