using System;

namespace TelegramApi.TLCore.Extensions
{
    public static class DateTimeExtensions
    {
        public static Int64 ToUnixTime(this DateTime dateTime)
        {
            return (Int64)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }
    }
}