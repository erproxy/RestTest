using System;
using System.Collections.Generic;

namespace Utils.Extensions
{
    public static class DateTimeExtensions
    {
        public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static long ToUnixTimestamp(this DateTime time)
        {
            return (long)time.Subtract(UnixEpoch).TotalSeconds;
        }

        public static DateTime SetFullHours(this DateTime time, int hour)
        {
            return new DateTime(time.Year, time.Month, time.Day, hour, 0, 0, time.Kind);
        }
        
        public static DateTime Max(DateTime first, DateTime second) {
            if (Comparer<DateTime>.Default.Compare(first, second) > 0)
                return first;
            return second;
        }
    }
}