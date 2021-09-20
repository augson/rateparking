using Microsoft.EntityFrameworkCore;
using ParkingEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using ParkingEntity.Enums;
namespace ParkingEntity.Context
{
    public class ParkingRateContext : DbContext, IParkingRateContext
    {
        public ParkingRateContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ParkingRate> ParkingRate { get; set; }
        public DbSet<ParkingRateRule> ParkingRateRule { get; set; }
        public DbSet<ParkingRateRuleDefinition> ParkingRateRuleDefinition { get; set; }

        /// <summary>
        /// Seeding the intial set of Rule definition for the rate calculation
        /// </summary>
        /// <param name="modelBuilder"></param>

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var parkingStandardRate = new ParkingRate("StandardRate", true, RateType.HourlyRate);
            var parkingNightRate = new ParkingRate("NightRate", true, RateType.FlatRate);
            var parkingEarlyBirdRate = new ParkingRate("EarlyBirdRate", true, RateType.FlatRate);
            var parkingWeekEndRate = new ParkingRate("WeekdEndRate", true, RateType.FlatRate);
            modelBuilder.Entity<ParkingRate>().HasData(parkingStandardRate, parkingNightRate,
                parkingEarlyBirdRate, parkingWeekEndRate);

            modelBuilder.Entity<ParkingRateRule>(
                entity =>
                {
                    entity.HasOne(d => d.ParkingRate)
                        .WithMany(p => p.ParkingRateRule)
                        .HasForeignKey("ParkingRateId");
                });
            //standard rate Rule
            var oneHourRateRule = new ParkingRateRule(parkingStandardRate.Id, "OneHourRateRule", 5, true);
            var twoHourRateRule = new ParkingRateRule(parkingStandardRate.Id, "TwoHourRateRule", 10, true);
            var threeHourRateRule = new ParkingRateRule(parkingStandardRate.Id, "ThreeHourRateRule", 15, true);
            var threePlusRateRule = new ParkingRateRule(parkingStandardRate.Id, "ThreePlusHourRateRule", 20, true);

            // Night Rate rule
            var nightRateRule = new ParkingRateRule(parkingNightRate.Id, "NightRateRule", 6.50, true);

            // Early Bird Rate rule
            var earlyBirdRateRule = new ParkingRateRule(parkingEarlyBirdRate.Id, "EarlyBirdRateRule", 13, true);

            // Weekend Rate rule
            var weekendRateRule = new ParkingRateRule(parkingWeekEndRate.Id, "WeekEndRateRule", 10, true);

            modelBuilder.Entity<ParkingRateRuleDefinition>(
               entity =>
               {
                   entity.HasOne(d => d.ParkingRateRule)
                       .WithMany(p => p.ParkingRateRuleDefinition)
                       .HasForeignKey("ParkingRateRuleId");
               });

            modelBuilder.Entity<ParkingRateRule>().HasData(oneHourRateRule, twoHourRateRule,
                threeHourRateRule, threePlusRateRule, nightRateRule, earlyBirdRateRule, weekendRateRule);

            //Standard Rate Rule Definition
            var oneHourRuleDefinition1 = new ParkingRateRuleDefinition(oneHourRateRule.Id, RateCondition.ParkingDuration, RateLinkingCondition.None,
                RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateDouble, "1", 1);
            var twoHourRuleDefinition1 = new ParkingRateRuleDefinition(twoHourRateRule.Id, RateCondition.ParkingDuration, RateLinkingCondition.And,
                RateComparisionCondition.GreaterThanOrEqualTo, RateValueComparisonType.RateDouble, "1", 1);
            var twoHourRuleDefinition2 = new ParkingRateRuleDefinition(twoHourRateRule.Id, RateCondition.ParkingDuration, RateLinkingCondition.And,
                RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateDouble, "2", 2);
            var threeHourRuleDefinition1 = new ParkingRateRuleDefinition(threeHourRateRule.Id, RateCondition.ParkingDuration, RateLinkingCondition.And,
                    RateComparisionCondition.GreaterThanOrEqualTo, RateValueComparisonType.RateDouble, "2", 1);
            var threeHourRuleDefinition2 = new ParkingRateRuleDefinition(threeHourRateRule.Id, RateCondition.ParkingDuration, RateLinkingCondition.And,
                     RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateDouble, "3", 2);
            var threePlusHourRuleDefinition1 = new ParkingRateRuleDefinition(threePlusRateRule.Id, RateCondition.ParkingDuration, RateLinkingCondition.None,
                   RateComparisionCondition.GreaterThanOrEqualTo, RateValueComparisonType.RateDouble, "3", 1);

            //Night Rate Rule definition

            var nighRateRuleDefinition1 = new ParkingRateRuleDefinition(nightRateRule.Id, RateCondition.StartDateStartTime, RateLinkingCondition.And,
                RateComparisionCondition.GreaterThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "18:00", 1);
            var nighRateRuleDefinition2 = new ParkingRateRuleDefinition(nightRateRule.Id, RateCondition.StartDateEndTime, RateLinkingCondition.And,
                RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "23:59", 2, true, true);
            var nighRateRuleDefinition3 = new ParkingRateRuleDefinition(nightRateRule.Id, RateCondition.EndDateEndTime, RateLinkingCondition.And,
              RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "08:00", 3, true, true);
            var nighRateRuleDefinition4 = new ParkingRateRuleDefinition(nightRateRule.Id, RateCondition.IsWeekDayStartDate, RateLinkingCondition.And,
              RateComparisionCondition.EqualTo, RateValueComparisonType.IsWeekDay, "true", 4);
            var nighRateRuleDefinition5 = new ParkingRateRuleDefinition(nightRateRule.Id, RateCondition.IsWeekDayEndDate, RateLinkingCondition.And,
             RateComparisionCondition.EqualTo, RateValueComparisonType.IsWeekDay, "true", 4);



            //early Bird Rate Rule definition

            var earlyBirdRateRuleDefinition1 = new ParkingRateRuleDefinition(earlyBirdRateRule.Id, RateCondition.StartDateStartTime, RateLinkingCondition.And,
                RateComparisionCondition.GreaterThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "6:00", 1);
            var earlyBirdRateRuleDefinition2 = new ParkingRateRuleDefinition(earlyBirdRateRule.Id, RateCondition.StartDateEndTime, RateLinkingCondition.And,
               RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "9:00", 2);
            var earlyBirdRateRuleDefinition3 = new ParkingRateRuleDefinition(earlyBirdRateRule.Id, RateCondition.EndDateStartTime, RateLinkingCondition.And,
              RateComparisionCondition.GreaterThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "15:30", 3);
            var earlyBirdRateRuleDefinition4 = new ParkingRateRuleDefinition(earlyBirdRateRule.Id, RateCondition.EndDateEndTime, RateLinkingCondition.And,
            RateComparisionCondition.LessThanOrEqualTo, RateValueComparisonType.RateTimeSpan, "23:30", 4);


            //weekend Rate Rule definition

            var weekendRateRuleDefinition1 = new ParkingRateRuleDefinition(weekendRateRule.Id, RateCondition.IsWeekendStartDate, RateLinkingCondition.And,
                RateComparisionCondition.EqualTo, RateValueComparisonType.IsWeekend, "true", 1);
            var weekendRateRuleDefinition2 = new ParkingRateRuleDefinition(weekendRateRule.Id, RateCondition.IsWeekendEndate, RateLinkingCondition.And,
              RateComparisionCondition.EqualTo, RateValueComparisonType.IsWeekend, "true", 2);


            modelBuilder.Entity<ParkingRateRuleDefinition>().HasData(oneHourRuleDefinition1,
                twoHourRuleDefinition1, twoHourRuleDefinition2, threeHourRuleDefinition1,
                threeHourRuleDefinition2, threePlusHourRuleDefinition1, nighRateRuleDefinition1,
                nighRateRuleDefinition2, nighRateRuleDefinition3, earlyBirdRateRuleDefinition1,
                earlyBirdRateRuleDefinition2, earlyBirdRateRuleDefinition3, earlyBirdRateRuleDefinition4,
                weekendRateRuleDefinition1, weekendRateRuleDefinition2, nighRateRuleDefinition4, nighRateRuleDefinition5);




            base.OnModelCreating(modelBuilder);

        }

        public void AddCascadingObject(object rootEntity) //Place inside DbContext.cs
        {
            ChangeTracker.TrackGraph(
                rootEntity,
                node =>
                    node.Entry.State = !node.Entry.IsKeySet ? EntityState.Added : EntityState.Unchanged
            );
        }

    }
}
