using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Util
{
    public static class DateUtil
    {
        public static bool IsWeekDay(this DateTime dateTime)
        {
            return dateTime.DayOfWeek >= DayOfWeek.Monday && dateTime.DayOfWeek <= DayOfWeek.Friday;
        }

        public static bool IsWeekEndDay(this DateTime dateTime)
        {
            return dateTime.DayOfWeek == DayOfWeek.Saturday || dateTime.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime? GetValidDateTime(this string strDateTime, bool isDateTimeValidate)
        {
            if (!isDateTimeValidate || strDateTime.IsValidDateTime())
            {
                DateTime.TryParse(strDateTime, out DateTime dateTime);
                return dateTime;
            }

            return null;
        }
        public static bool IsValidDateTime(this string strDateTime)
        {
            return DateTime.TryParse(strDateTime, out DateTime dateTime);
        }
    }
}
