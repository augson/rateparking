using Microsoft.EntityFrameworkCore;
using ParkingEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingEntity.Context
{
   public interface IParkingRateContext
    {
       
        public DbSet<ParkingRate> ParkingRate { get; set; }
        public DbSet<ParkingRateRule> ParkingRateRule { get; set; }
        public DbSet<ParkingRateRuleDefinition> ParkingRateRuleDefinition { get; set; }
    }
}
