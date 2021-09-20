using ParkingEntity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingEntity.Entity
{
    /// <summary>
    /// Class for the presitence different rule definition for a Parking rate rule to address
    /// </summary>
    public class ParkingRateRuleDefinition : ISqlEntity
    {
        public virtual Guid Id { get;  set; }
        public virtual ParkingRateRule ParkingRateRule { get;  set; }
        public virtual Guid ParkingRateRuleId { get;  set; }
        [Column(TypeName = "varchar(255)")]
        public virtual RateCondition RateCondition { get;  set; }

        [Column(TypeName = "varchar(255)")]
        public virtual RateLinkingCondition RateLinkingCondition { get;  set; }

        [Column(TypeName = "varchar(255)")]
        public virtual RateComparisionCondition RateComparisionCondition { get;  set; }

        public virtual string ComparisonValue { get;  set; }

        [Column(TypeName = "varchar(255)")]
        public virtual RateValueComparisonType RateValueComparisonType { get;  set; }

        public virtual int RuleDefinitionOrder { get;  set; }

        public virtual bool IsConverToDateAndCheck { get;  set; }

        public virtual bool IsEndDateToBeAddedToOneDay { get;  set; }

        /// <summary>
        /// default constructor
        /// </summary>
        public ParkingRateRuleDefinition()
        {

        }
        /// <summary>
        /// New Object Creation
        /// </summary>
        /// <param name="parkingRateRuleId"></param>
        /// <param name="rateCondition"></param>
        /// <param name="rateLinkingCondition"></param>
        /// <param name="rateComparisionCondition"></param>
        /// <param name="rateValueComparisonType"></param>
        /// <param name="comparisonValue"></param>
        /// <param name="ruleDefinitionOrder"></param>
        public ParkingRateRuleDefinition(Guid parkingRateRuleId, RateCondition rateCondition,
            RateLinkingCondition rateLinkingCondition, RateComparisionCondition rateComparisionCondition,
            RateValueComparisonType rateValueComparisonType, string comparisonValue, int ruleDefinitionOrder,
            bool isConverToDateAndCheck=false, bool isEndDateToBeAddedToOneDay=false
          )
        {
            Id = Guid.NewGuid();
            ParkingRateRuleId = parkingRateRuleId;
            RateCondition = rateCondition;
            RateLinkingCondition = rateLinkingCondition;
            RateComparisionCondition = rateComparisionCondition;
            RateValueComparisonType = rateValueComparisonType;
            ComparisonValue = comparisonValue;
            RuleDefinitionOrder = ruleDefinitionOrder;
            IsConverToDateAndCheck = isConverToDateAndCheck;
            IsEndDateToBeAddedToOneDay = isEndDateToBeAddedToOneDay;

        }

    }
}
