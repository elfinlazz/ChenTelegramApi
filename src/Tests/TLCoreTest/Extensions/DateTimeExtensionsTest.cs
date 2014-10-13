using System;
using FluentAssertions;
using NUnit.Framework;
using TelegramApi.TLCore.Extensions;

namespace TelegramApi.TLCoreTest.Extensions
{
    public class DateTimeExtensionsTest
    {
        [Test]
        public void ToUnixTime_ThenCorrectUnixTimeReturned()
        {
            // arrange
            const Int64 Expected = 1396137600;
            DateTime dateTime = new DateTime(2014, 03, 30, 0, 0, 0, 0);

            // act
            Int64 unixTime = dateTime.ToUnixTime();

            // assert
            unixTime.ShouldBeEquivalentTo(Expected);
        }
    }
}