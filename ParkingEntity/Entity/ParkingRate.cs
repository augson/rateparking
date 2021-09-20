using ParkingEntity.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParkingEntity.Entity
{
    public class ParkingRate:ISqlEntity
    {
        public virtual Guid Id { get;   set; }

        [Column(TypeName = "varchar(255)")]
        public virtual string Name { get;  set; }
        public virtual  bool IsValid { get;  set; }

        [Column(TypeName = "varchar(255)")]
        public virtual RateType RateType { get; set; }
        //Default Constructor
        public ParkingRate()
        {

        }

        public ParkingRate(string name,bool isValid,RateType rateType)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsValid = isValid;
            RateType = rateType;
        }
        public  virtual ICollection<ParkingRateRule> ParkingRateRule { get; set; }
    }
}
