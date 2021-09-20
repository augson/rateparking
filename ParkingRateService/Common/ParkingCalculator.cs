using Infrastructure.Util;
using ParkingEntity.Dto;
using ParkingEntity.Entity;
using ParkingEntity.Enums;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingApplicationService.Common
{
    /// <summary>
    /// Dynamic Rule Calculator for parking charge rate identification
    /// </summary>
    public static class ParkingCalculator
    {
        public static List<ParkingRateRule> GetMatchingParkingRate(this CustomDate customDate, List<ParkingRate> parkingRates)
        {
            var parkingRuleList = new ConcurrentBag<ParkingRateRule>();

            Parallel.ForEach(parkingRates, parkingRate =>
            {

                parkingRuleList.AddRange(GetMatchingParkingRateRules(customDate, parkingRate));


            });

            return parkingRuleList.ToList();
        }


       public static List<ParkingRateRule>GetMatchingParkingRateRules(CustomDate customDate, ParkingRate parkingRate)
        {
            var parkingRuleList = new List<ParkingRateRule>();
            var parkingRules = parkingRate.ParkingRateRule.ToList();
            foreach (var parkingRule in parkingRules)
            {
                var parkingRuleDefinitions = parkingRule.ParkingRateRuleDefinition.ToList();

                var trueList = GetMatchingRuleDefinitions(parkingRuleDefinitions, customDate);
                if (trueList.All(s => s) == true) parkingRuleList.Add(parkingRule);

            }
            return parkingRuleList;

        }

        public static List<bool>GetMatchingRuleDefinitions(List<ParkingRateRuleDefinition> parkingRuleDefinitions,CustomDate customDate)
        {
            var trueList = new List<bool>();

            foreach (var ruleDefinition in parkingRuleDefinitions)
            {
                if (ruleDefinition.RateCondition == RateCondition.StartDate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<DateTime>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(GetDateComparison(ruleDefinifationValue, customDate.StartDate,
                         ruleDefinition.RateComparisionCondition));
                }
                if (ruleDefinition.RateCondition == RateCondition.EndDate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<DateTime>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(GetDateComparison(ruleDefinifationValue, customDate.EndDate,
                       ruleDefinition.RateComparisionCondition));
                }
                if (ruleDefinition.RateCondition == RateCondition.StartDateEndTime)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<TimeSpan>(ruleDefinition.RateValueComparisonType);
                    if (ruleDefinition.IsConverToDateAndCheck)
                    {
                        var endDateTime = new DateTime(customDate.StartDate.Year, customDate.StartDate.Month,
                            customDate.StartDate.Day).Add(ruleDefinifationValue).AddDays(1);
                        trueList.Add(GetDateComparison(customDate.StartDate, endDateTime,
                          ruleDefinition.RateComparisionCondition));

                    }
                    else
                    {
                        trueList.Add(GetTimeSpanComparison(customDate.StartingHour, ruleDefinifationValue,
                       ruleDefinition.RateComparisionCondition));
                    }
                }
                if (ruleDefinition.RateCondition == RateCondition.StartDateStartTime)
                {

                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<TimeSpan>(ruleDefinition.RateValueComparisonType);

                    trueList.Add(GetTimeSpanComparison(customDate.StartingHour, ruleDefinifationValue,
                   ruleDefinition.RateComparisionCondition));


                }
                if (ruleDefinition.RateCondition == RateCondition.EndDateStartTime)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<TimeSpan>(ruleDefinition.RateValueComparisonType);
                    if (ruleDefinition.IsConverToDateAndCheck)
                    {
                        var endDateTime = new DateTime(customDate.EndDate.Year, customDate.EndDate.Month,
                            customDate.EndDate.Day).Add(ruleDefinifationValue).AddDays(1);
                        trueList.Add(GetDateComparison(customDate.StartDate, endDateTime,
                          ruleDefinition.RateComparisionCondition));

                    }
                    else
                    {
                        trueList.Add(GetTimeSpanComparison(customDate.FinishingHour, ruleDefinifationValue,
                            ruleDefinition.RateComparisionCondition));
                    }
                }
                if (ruleDefinition.RateCondition == RateCondition.EndDateEndTime)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<TimeSpan>(ruleDefinition.RateValueComparisonType);
                    if (ruleDefinition.IsConverToDateAndCheck)
                    {
                        var endDateTime = new DateTime(customDate.EndDate.Year, customDate.EndDate.Month,
                            customDate.EndDate.Day).Add(ruleDefinifationValue).AddDays(1);
                        trueList.Add(GetDateComparison(customDate.EndDate, endDateTime,
                          ruleDefinition.RateComparisionCondition));

                    }
                    else
                    {
                        trueList.Add(GetTimeSpanComparison(customDate.FinishingHour, ruleDefinifationValue,
                            ruleDefinition.RateComparisionCondition));
                    }

                }
                if (ruleDefinition.RateCondition == RateCondition.ParkingDuration)
                {
                    var ruleDefinitionValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<double>(ruleDefinition.RateValueComparisonType);

                    trueList.Add(GetDoubleComparison(customDate.ParkingDuration, ruleDefinitionValue,
                         ruleDefinition.RateComparisionCondition));


                }
                if (ruleDefinition.RateCondition == RateCondition.IsWeekendStartDate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<bool>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(ruleDefinifationValue == customDate.IsWeekEndStartDate);

                }
                if (ruleDefinition.RateCondition == RateCondition.IsWeekendEndate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<bool>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(ruleDefinifationValue == customDate.IsWeekEndEndDate);

                }
                if (ruleDefinition.RateCondition == RateCondition.IsWeekendStartDate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<bool>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(ruleDefinifationValue == customDate.IsWeekEndStartDate);

                }
                if (ruleDefinition.RateCondition == RateCondition.IsWeekDayStartDate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<bool>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(ruleDefinifationValue == customDate.IsWeekDayStartDate);

                }
                if (ruleDefinition.RateCondition == RateCondition.IsWeekDayEndDate)
                {
                    var ruleDefinifationValue = ruleDefinition.ComparisonValue.
                    GetRuleDefinitionValue<bool>(ruleDefinition.RateValueComparisonType);
                    trueList.Add(ruleDefinifationValue == customDate.IsWeekDayEndtDate);

                }

            }
            return trueList;
        }


        #region Helper Functions
        private static bool GetDateComparison(DateTime cfConfgured, DateTime cfValue, RateComparisionCondition condition)
        {

            if (condition == RateComparisionCondition.EqualTo)
            {
                return cfConfgured == cfValue;
            }
            else if (condition == RateComparisionCondition.GreaterThanOrEqualTo)
            {
                return cfConfgured >= cfValue;
            }
            else if (condition == RateComparisionCondition.LessThanOrEqualTo)
            {
                return cfConfgured <= cfValue;
            }
            return false;
        }

        private static bool GetTimeSpanComparison(TimeSpan cfConfgured, TimeSpan cfValue, RateComparisionCondition condition)
        {

            if (condition == RateComparisionCondition.EqualTo)
            {
                return cfConfgured == cfValue;
            }
            else if (condition == RateComparisionCondition.GreaterThanOrEqualTo)
            {
                return cfConfgured >= cfValue;
            }
            else if (condition == RateComparisionCondition.LessThanOrEqualTo)
            {
                return cfConfgured <= cfValue;
            }
            return false;
        }
        private static bool GetDoubleComparison(double cfConfgured, double cfValue, RateComparisionCondition condition)
        {

            if (condition == RateComparisionCondition.EqualTo)
            {
                return cfConfgured == cfValue;
            }
            else if (condition == RateComparisionCondition.GreaterThanOrEqualTo)
            {
                return cfConfgured >= cfValue;
            }
            else if (condition == RateComparisionCondition.LessThanOrEqualTo)
            {
                return cfConfgured <= cfValue;
            }
            return false;
        }


        /// <summary>
        /// Method to convert rule definition value into standard types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="rateValueComparisonType"></param>
        /// <returns></returns>
        private static T GetRuleDefinitionValue<T>(this string value, RateValueComparisonType rateValueComparisonType)
        {
            try
            {

                switch (rateValueComparisonType)
                {
                    case RateValueComparisonType.IsWeekDay:
                    case RateValueComparisonType.IsWeekend:
                        {

                            bool boolVal;
                            if (value.Trim() == "1")
                            {
                                value = "True";
                            }
                            bool.TryParse(value, out boolVal);
                            return (T)(object)boolVal;
                        }
                    case RateValueComparisonType.RateDouble:
                        {

                            double doubleVal;
                            double.TryParse(value, out doubleVal);
                            return (T)(object)doubleVal;
                        }
                    case RateValueComparisonType.RateTimeSpan:
                        {
                            TimeSpan timeSpan;
                            TimeSpan.TryParse(value, out timeSpan);
                            return (T)(object)timeSpan;
                        }
                    case RateValueComparisonType.RateString:
                        {
                            return (T)(object)value;
                        }
                    case RateValueComparisonType.RateDateTime:
                        {
                            DateTime dateTime;
                            DateTime.TryParse(value, out dateTime);
                            return (T)(object)dateTime;
                        }
                    default:
                        return default(T);
                }
            }
            catch
            {
                return default(T);
            }


        }
        #endregion


    }
}
