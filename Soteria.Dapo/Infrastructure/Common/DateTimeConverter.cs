using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soteria.DataComponents.Infrastructure.Common
{
    public class DateTimeConverter
    {
        public static TimeZoneInfo GetFacilityTimeZone(string timeZoneName)
        {
            TimeZoneInfo sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneName);
            return sourceTimeZone;
        }

        public static DateTime ConvertDateTimeToUTC(DateTime datetime, TimeZoneInfo timeZoneToConvert)
        {
            return TimeZoneInfo.ConvertTimeToUtc(datetime, timeZoneToConvert);
        }

        public static DateTime ConvertUTCToTimeZone(DateTime datetime, TimeZoneInfo timeZoneToConvert)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(datetime, timeZoneToConvert);
        }

    }
}
