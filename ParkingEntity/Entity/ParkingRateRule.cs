using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingEntity.Entity
{
    public class ParkingRateRule : ISqlEntity
    {
        public virtual Guid Id { get;  set; }
        public virtual ParkingRate ParkingRate { get;  set; }
        public virtual Guid ParkingRateId { get;  set; }

        [Column(TypeName = "varchar(255)")]
        public virtual string RuleName { get;  set; }
        public virtual double Rate { get;  set; }
        public virtual bool IsValid { get;  set; }

        public ParkingRateRule()
        {

        }

        public ParkingRateRule(Guid parkingRateId,string ruleName, double rate,bool isValid)
        {
            Id = Guid.NewGuid();

            ParkingRateId = parkingRateId;
            RuleName = ruleName;
            Rate = rate;
            IsValid = isValid; 
        }
        public virtual ICollection<ParkingRateRuleDefinition> ParkingRateRuleDefinition { get; set; }
    }
}
