
using Infrastructure.Util;
using System;

namespace ParkingEntity.Dto
{
    /// <summary>
    /// A Class to Capture all the Rate Date Condition to Process
    /// </summary>
    public class CustomDate
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public double ParkingDuration { get; private set; }
        public DayOfWeek ParkingDay { get; private set; }
        public TimeSpan StartingHour { get; private set; }
        public TimeSpan FinishingHour { get; private set; }
        public bool IsValidTime { get; private set; }
        public bool IsWeekDayStartDate { get; private set; }
        public bool IsWeekDayEndtDate { get; private set; }
        public bool IsWeekEndStartDate { get; private set; }
        public bool IsWeekEndEndDate { get; private set; }
        /// <summary>
        /// Default Contructor
        /// </summary>
        public CustomDate()
        {

        }
        public CustomDate(DateTime startDateTime,DateTime endDateTime)
        {
            Fill(startDateTime, endDateTime);

        }

        private void Fill(DateTime startDate,DateTime enddateTime)
        {
            IsValidTime = (enddateTime - startDate).TotalHours > 0;
            if (IsValidTime)
            {
                StartDate = startDate;
                EndDate = enddateTime;
                ParkingDuration = (enddateTime - startDate).TotalHours;
                StartingHour = startDate.TimeOfDay;
                FinishingHour = enddateTime.TimeOfDay;
                IsWeekDayStartDate = startDate.IsWeekDay();
                IsWeekDayEndtDate = enddateTime.IsWeekDay();
                ParkingDay = startDate.DayOfWeek;
                IsWeekEndStartDate = startDate.IsWeekEndDay();
                IsWeekEndEndDate = EndDate.IsWeekEndDay();
            }
        }


        
    }
}
