using DomainService.Implementation;
using DomainService.Interface;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using ParkingApplication.Implementation;
using ParkingApplication.Interface;
using ParkingEntity.Context;
using ParkingEntity.Entity;
using ParkingEntity.Enums;
using System.Collections.Generic;
using System.Linq;

namespace ParkingRateTest
{
    /// <summary>
    /// Parking Rate Test cases
    /// </summary>
    public class ParkingTests
    {
        private readonly IParkingCalculatorService _parkingCalculatorService;
        private List<ParkingRate> ParkingRates;
        public ParkingTests()
        {
            var serviceProvider = new ServiceCollection()
            .AddLogging().
            AddSingleton<IParkingCalculatorService, ParkingCalclatorService>().
            AddSingleton<IParkingRateService, ParkingRateService>().
            AddSingleton<IParkingRateContext, ParkingRateContext>().
            AddDbContext<ParkingRateContext>(opts => opts.
                UseSqlServer("put test db connection here"))
           .BuildServiceProvider();



            _parkingCalculatorService = serviceProvider.GetService<IParkingCalculatorService>();
        }
        
        [SetUp]
        public void Setup()
        {
            ParkingRates = PopulateRulesData();
        }

        [Test]
        [TestCase("20/09/2021 09:50 AM", "20/09/2021 13:50 PM", 20)]
        [TestCase("20/09/2021 09:50 AM", "20/09/2021 10:40 AM", 5)]
        [TestCase("20/09/2021 09:50 AM", "20/09/2021 11:40 AM", 10)]
        [TestCase("20/09/2021 09:50 AM", "20/09/2021 12:40 PM", 15)]
        public void GetStandardRateTest(string strEntry, string strExit, double value)
        {
            var parkingRate = ParkingRates;
            var matchingRate = _parkingCalculatorService.GetApplicatbleParkingRates(strEntry.GetValidDateTime(true).Value,
                strExit.GetValidDateTime(true).Value, ParkingRates);
            if (matchingRate.Count > 0)
            {
                var lowestMatchingRate = matchingRate.OrderBy(s => s.Rate).First();
                Assert.AreEqual(lowestMatchingRate.Rate, value);
            }

        }
        [TestCase("20/09/2021 07:50 AM", "20/09/2021 17:50 PM",13)]
        [TestCase("20/09/2021 08:50 AM", "20/09/2021 10:40 PM",13)]
        [TestCase("20/09/2021 06:50 AM", "20/09/2021 11:20 PM",13)]
    
        public void GetEarlyBirdRateTest(string strEntry, string strExit, double value)
        {
            var parkingRate = ParkingRates;
            var matchingRate = _parkingCalculatorService.GetApplicatbleParkingRates(strEntry.GetValidDateTime(true).Value,
                strExit.GetValidDateTime(true).Value, ParkingRates);
            if (matchingRate.Count > 0)
            {
                var lowestMatchingRate = matchingRate.OrderBy(s => s.Rate).First();
                Assert.AreEqual(lowestMatchingRate.Rate, value);
            }

        }

        [TestCase("20/09/2021 07:50 PM", "20/09/2021 11:50 PM",  6.50)]
        [TestCase("20/09/2021 07:50 PM", "20/09/2021 09:50 PM",  6.50)]
        [TestCase("20/09/2021 07:50 PM", "21/09/2021 07:50 AM", 6.50)]      
        public void GetNightRateTest(string strEntry, string strExit, double value)
        {
            var parkingRate = ParkingRates;
            var matchingRate = _parkingCalculatorService.GetApplicatbleParkingRates(strEntry.GetValidDateTime(true).Value,
                strExit.GetValidDateTime(true).Value, ParkingRates);
            if (matchingRate.Count > 0)
            {
                var lowestMatchingRate = matchingRate.OrderBy(s => s.Rate).First();
                Assert.AreEqual(lowestMatchingRate.Rate, value);
            }

        }

        [TestCase("18/09/2021 00:50 AM", "19/09/2021 23:50 PM",  10)]
        [TestCase("18/09/2021 07:50 PM", "19/09/2021 07:50 PM", 10)]
        public void GetWeekEndRateTest(string strEntry, string strExit, double value)
        {
            var parkingRate = ParkingRates;
            var matchingRate = _parkingCalculatorService.GetApplicatbleParkingRates(strEntry.GetValidDateTime(true).Value,
                strExit.GetValidDateTime(true).Value, ParkingRates);
            if (matchingRate.Count > 0)
            {
                var lowestMatchingRate = matchingRate.OrderBy(s => s.Rate).First();
                Assert.AreEqual(lowestMatchingRate.Rate, value);
            }

        }

        #region Mock Data
        private List<ParkingRate> PopulateRulesData()
        {
            var parkList = new List<ParkingRate>
            {
               new ParkingRate
               {
                   Name = "StandardRate",
                   IsValid = true,
                   RateType = RateType.HourlyRate,
                   ParkingRateRule = new List<ParkingRateRule>
                   {
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 5,
                           RuleName = "OneHourRateRule",
                           ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.ParkingDuration,
                                   RateLinkingCondition = RateLinkingCondition.None,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateDouble,
                                   ComparisonValue ="1",
                                   RuleDefinitionOrder = 1
                               }

                           }

                       },
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 10,
                           RuleName = "TwoHourRateRule",
                           ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.ParkingDuration,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.GreaterThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateDouble,
                                   ComparisonValue ="1",
                                   RuleDefinitionOrder = 1
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.ParkingDuration,
                                   RateLinkingCondition = RateLinkingCondition.None,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateDouble,
                                   ComparisonValue ="2",
                                   RuleDefinitionOrder = 2
                               }
                           }

                       },
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 15,
                           RuleName = "ThreeHourRateRule",
                           ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.ParkingDuration,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.GreaterThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateDouble,
                                   ComparisonValue ="2",
                                   RuleDefinitionOrder = 1
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.ParkingDuration,
                                   RateLinkingCondition = RateLinkingCondition.None,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateDouble,
                                   ComparisonValue ="3",
                                   RuleDefinitionOrder = 2
                               }
                           }
                       },
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 20,
                           RuleName = "ThreePlusHourRateRule",
                           ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.ParkingDuration,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.GreaterThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateDouble,
                                   ComparisonValue ="3",
                                   RuleDefinitionOrder = 1
                               },

                           }
                       },
                   }
               },
               new ParkingRate{
                   IsValid = true,
                   Name = "NightRate",
                    RateType = RateType.FlatRate,
                   ParkingRateRule = new List<ParkingRateRule>
                   {
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 6.50,
                           RuleName = "NightRateRule",
                            ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.StartDateStartTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.GreaterThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="18:00",
                                   RuleDefinitionOrder = 1
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.StartDateEndTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="23:59",
                                   RuleDefinitionOrder = 2,
                                   IsConverToDateAndCheck = true,
                                   IsEndDateToBeAddedToOneDay =true
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.EndDateEndTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="08:00",
                                   RuleDefinitionOrder = 3,
                                   IsConverToDateAndCheck = true,
                                   IsEndDateToBeAddedToOneDay =true
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.IsWeekDayStartDate,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.EqualTo,
                                   RateValueComparisonType = RateValueComparisonType.IsWeekDay,
                                   ComparisonValue ="true",
                                   RuleDefinitionOrder = 4
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.IsWeekDayEndDate,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.EqualTo,
                                   RateValueComparisonType = RateValueComparisonType.IsWeekDay,
                                   ComparisonValue ="true",
                                   RuleDefinitionOrder = 5
                               },

                           }
                       },
                   }
               },
               new ParkingRate{
                   IsValid = true,
                   Name = "EarlyBirdRate",
                    RateType = RateType.FlatRate,
                   ParkingRateRule = new List<ParkingRateRule>
                   {
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 13,
                           RuleName = "EarlyBirdRateRule",
                            ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.StartDateStartTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.GreaterThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="6:00",
                                   RuleDefinitionOrder = 1
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.StartDateEndTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="9:00",
                                   RuleDefinitionOrder = 2,

                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.EndDateStartTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.GreaterThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="15:30",
                                   RuleDefinitionOrder = 2,

                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.EndDateEndTime,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.LessThanOrEqualTo,
                                   RateValueComparisonType = RateValueComparisonType.RateTimeSpan,
                                   ComparisonValue ="23:30",
                                   RuleDefinitionOrder = 3,
                                   IsConverToDateAndCheck = true,
                                   IsEndDateToBeAddedToOneDay =true
                               },
                            },
                       }
                   }
               },

               new ParkingRate{
                   IsValid = true,
                   Name = "WeekdEndRate",
                    RateType = RateType.FlatRate,
                   ParkingRateRule = new List<ParkingRateRule>
                   {
                       new ParkingRateRule
                       {
                           IsValid = true,
                           Rate = 10,
                           RuleName = "WeekEndRateRule",
                           ParkingRateRuleDefinition = new List<ParkingRateRuleDefinition>
                           {
                              new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.IsWeekendStartDate,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.EqualTo,
                                   RateValueComparisonType = RateValueComparisonType.IsWeekend,
                                   ComparisonValue ="true",
                                   RuleDefinitionOrder = 1
                               },
                               new ParkingRateRuleDefinition
                               {
                                   RateCondition = RateCondition.IsWeekendEndate,
                                   RateLinkingCondition = RateLinkingCondition.And,
                                   RateComparisionCondition = RateComparisionCondition.EqualTo,
                                   RateValueComparisonType = RateValueComparisonType.IsWeekend,
                                   ComparisonValue ="true",
                                   RuleDefinitionOrder = 1
                               },

                           }
                       },

                   }
               },

            };
            return parkList;

        }
        #endregion
    }
}