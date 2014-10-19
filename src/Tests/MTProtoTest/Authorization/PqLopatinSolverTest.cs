using System;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.MTProto.Authorization;

namespace TelegramApi.MTProtoTest.Authorization
{
    public class PqLopatinSolverTest
    {
        private PqLopatinSolver _testee;

        [SetUp]
        public void SetUp()
        {
            _testee = new PqLopatinSolver();
        }

        [Test]
        public void SolvePq_ThenPqSolved()
        {
            // arrange
            const UInt64 Input = 0x17ED48941A08F981;
            const UInt32 ExpectedP = 0x494C553B;
            const UInt32 ExpectedQ = 0x53911073;

            // act
            PqData result = _testee.SolvePq(Input);

            // assert
            result.P.ShouldBeEquivalentTo(ExpectedP);
            result.Q.ShouldBeEquivalentTo(ExpectedQ);
        }
    }
}