using System;
using Xunit;

namespace CTAM.Core.Utilities
{
    public class DateTimeUtilsTest
    {
        public struct DateTimeWithZone
        {
            private readonly DateTime utcDateTime;
            private readonly TimeZoneInfo timeZone;

            public DateTimeWithZone(DateTime dateTime, TimeZoneInfo timeZone)
            {
                var dateTimeUnspec = DateTime.SpecifyKind(dateTime, DateTimeKind.Unspecified);
                utcDateTime = TimeZoneInfo.ConvertTimeToUtc(dateTimeUnspec, timeZone);
                this.timeZone = timeZone;
            }

            public DateTime UniversalTime { get { return utcDateTime; } }

            public TimeZoneInfo TimeZone { get { return timeZone; } }

            public DateTime LocalTime
            {
                get
                {
                    return TimeZoneInfo.ConvertTime(utcDateTime, timeZone);
                }
            }
        }

        public static readonly object[][] DTTimeStampDefaults =
        {
            new object[] { new DateTime(2021, 1, 1,0,0,0), "1-1-2021 01:00:00" },
            new object[] { new DateTime(2021,12,31,23,59,59), "1-1-2022 00:59:59" },
            new object[] { new DateTime(2022, 2, 2,12,30,0), "2-2-2022 13:30:00" },
            new object[] { new DateTime(2022, 6, 1,0,0,0), "1-6-2022 02:00:00" },
            new object[] { new DateTime(2030, 1, 1,0,0,0), "1-1-2030 01:00:00" },
            new object[] { new DateTime(2040, 3, 14,3,14,15), "14-3-2040 04:14:15" }
        };

        public static readonly object[][] DTTimeStampTimeZone =
        {
            new object[] { new DateTime(2021, 1, 1,0,0,0), "UTC", "en-US", "1/1/2021 00:00:00" },
            new object[] { new DateTime(2021,12,31,23,59,59), "UTC", "en-US", "12/31/2021 23:59:59" },
            new object[] { new DateTime(2022, 2, 2,12,30,0), "Central Europe Standard Time", "fr-BE", "2/02/2022 13:30:00" },
            new object[] { new DateTime(2022, 6, 1,0,0,0), "Central Europe Standard Time", "fr-BE", "1/06/2022 02:00:00" },
            new object[] { new DateTime(2030, 1, 1,0,0,0), "Pacific Standard Time", "en-US", "12/31/2029 16:00:00" },
            new object[] { new DateTime(2040, 3, 14,3,14,15), "Pacific Standard Time", "en-US", "3/13/2040 20:14:15" }
        };

        public static readonly object[][] DTTimeStampInvalidTimeZoneCulture =
        {
            new object[] { new DateTime(2021, 1, 1,0,0,0, DateTimeKind.Utc), "bad zoneid", "nl-BE", "1/1/2021 00:00:00" },
            new object[] { new DateTime(2021,12,31,23,59,59, DateTimeKind.Utc), "bad zoneid", "nl-BE", "12/31/2021 23:59:59" },
            new object[] { new DateTime(2022, 2, 2,12,30,0, DateTimeKind.Utc), "Europe/Brussels", "bad culture", "2/2/2022 12:30:00" },
            new object[] { new DateTime(2022, 6, 1,0,0,0, DateTimeKind.Utc), "Europe/Brussels", "bad culture", "6/1/2022 00:00:00" },
            new object[] { new DateTime(2030, 1, 1,0,0,0, DateTimeKind.Utc), "", "", "1/1/2030 00:00:00" }
        };

        [Theory, MemberData(nameof(DTTimeStampDefaults))]
        public void TestValidDateTimeStamps(DateTime dt, string localdatetimestring)
        {
            //  Arrange
            // Get machine independent UTC time
            DateTime utcdt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);

            // Act
            var rc = utcdt.ToLocalDateTimeString();

            // Assert
            Assert.Equal(localdatetimestring, rc);
        }


        //[Theory, MemberData(nameof(DTTimeStampTimeZone))]
        //public void TestTimeZoneCultureTimeStamps(DateTime dt, string timezoneid, string localdatetimestring)
        //{
        //    //  Arrange
        //    // Get machine independent UTC time
        //    DateTime utcdt = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
        //    // Act
        //    var rc = utcdt.ToLocalDateTimeString(timezoneid);

        //    // Assert
        //    Assert.Equal(localdatetimestring, rc);
        //}


        //[Theory, MemberData(nameof(DTTimeStampInvalidTimeZoneCulture))]
        //public void TestInvalidTimeZoneCulture(DateTime dt, string timezoneid,string localdatetimestring)
        //{
        //    //  Arrange
        //    // because all testcases do only a ToString because of Timezone/culture exceptions we just need the passed UTC DT

        //    // Act
        //    var rc = dt.ToLocalDateTimeString(timezoneid);

        //    // Assert
        //    Assert.Equal(localdatetimestring, rc);
        //}

    }
}



