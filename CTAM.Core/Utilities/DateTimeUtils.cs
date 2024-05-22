using System;
using System.Globalization;
using CTAM.Core.Constants;
using TimeZoneConverter;

namespace CTAM.Core.Utilities
{
    public static class DateTimeUtils
    {
        // To do later on make this variable if we go international
        private const string DEFAULT_TIMEZONE = "W. Europe Standard Time";

        public static string ToLocalDateTimeString(this DateTime dateTime, string timeZoneId = DEFAULT_TIMEZONE)
        {
            try
            {
                TimeZoneInfo cetZone = TZConvert.GetTimeZoneInfo(timeZoneId);
                DateTime cetTime = TimeZoneInfo.ConvertTimeFromUtc(dateTime, cetZone);
                Console.WriteLine("The date and time are {0} {1}.",
                                  cetTime,
                                  cetZone.IsDaylightSavingTime(cetTime) ?
                                          cetZone.DaylightName : cetZone.StandardName);
                return cetTime.ToString(GetDateTimeFormat());
            }
            catch (TimeZoneNotFoundException)
            {
                Console.WriteLine($"ToLocalDateTimeString: The registry does not define the '{ timeZoneId}' zone.");
            }
            catch (InvalidTimeZoneException)
            {
                Console.WriteLine($"ToLocalDateTimeString: Registry data on the '{timeZoneId}' zone has been corrupted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ToLocalDateTimeString: {ex.Message}");
            }
            return dateTime.ToString(GetDateTimeFormat());
        }

        private static string GetDateTimeFormat()
        {
            var shortDatePattern = CulturedDateFormats.GetDateFormat(CultureInfo.CurrentCulture.Name);
            return $"{shortDatePattern} HH:mm:ss";
        }
    }
}
