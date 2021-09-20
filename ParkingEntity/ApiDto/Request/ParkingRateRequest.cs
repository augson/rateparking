using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingEntity.ApiDto.Request
{
   public class ParkingRateRequest
    {
        public string EntryDate { get; set; }
        public string ExitDate { get; set; }
    }
}
