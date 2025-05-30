using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Common
{
    public class ConvertUnixTime
    {
        public static long DateTimeToUnixTimeMilliseconds(DateTime dateTime)
        {
            return new DateTimeOffset(dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime()).ToUnixTimeMilliseconds();
        }

        public static long DateTimeStringToUnixTimeMilliseconds(string dateTimeStr)
        {
            if (DateTime.TryParse(dateTimeStr, out DateTime dateTime))
            {
                return new DateTimeOffset(dateTime.Kind == DateTimeKind.Utc ? dateTime : dateTime.ToUniversalTime()).ToUnixTimeMilliseconds();
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(dateTimeStr));
            }
        }
    }
}