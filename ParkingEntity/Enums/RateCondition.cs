using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingEntity.Enums
{
    public enum RateCondition
    {
        StartDate,
        EndDate,
        StartDateStartTime,
        StartDateEndTime,
        EndDateStartTime,
        EndDateEndTime,       
        ParkingDuration,
        DayOfWeek,
        IsWeekDayStartDate,
        IsWeekDayEndDate,
        IsWeekendStartDate,
        IsWeekendEndate


    }

    public enum RateLinkingCondition
    {
        And,
        Or,
       None,
    }
    public enum RateComparisionCondition
    {
        LessThanOrEqualTo,
        GreaterThanOrEqualTo,
        EqualTo,
        NotEqualTo,
        None
    }

    public enum RateValueComparisonType
    {
        RateTimeSpan,
        RateDateTime,
        RateString,
        RateDayOfWeek,
        RateDouble,
        IsWeekDay,
        IsWeekend,
    }


}
