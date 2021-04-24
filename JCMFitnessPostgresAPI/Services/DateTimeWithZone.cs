using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JCMFitnessPostgresAPI.Services
{
    public static class DateTimeWithZone
    {

        private static readonly TimeZoneInfo timeZone;

      /*  static DateTimeWithZone()
        {
            //I added web.config <add key="CurrentTimeZoneId" value="Central Europe Standard Time" />
            //You can add value directly into function.
            timeZone = TimeZoneInfo.FindSystemTimeZoneById("US Mountain Standard Time");
        }*/


        public static DateTime LocalTime(this DateTime t)
        {
            return TimeZoneInfo.ConvertTimeToUtc(t, TimeZoneInfo.Local);
        }
    }
}
