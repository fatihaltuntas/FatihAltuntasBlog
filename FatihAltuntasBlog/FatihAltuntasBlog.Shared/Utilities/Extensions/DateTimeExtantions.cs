using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FatihAltuntasBlog.Shared.Utilities.Extensions
{
    public static class DateTimeExtantions
    {
        public static string FullDateTimeStringWithUnderscore(this DateTime dateTime)
        {
            return $"{dateTime.Millisecond}-{dateTime.Second}-{dateTime.Minute}-{dateTime.Hour}-{dateTime.Day}-{dateTime.Month}-{dateTime.Year}";
        }
    }
}
