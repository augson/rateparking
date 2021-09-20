using System;
using System.Collections.Generic;
using System.Text;

namespace ParkingEntity.ApiDto.Response
{
    public class ParkingRateResponse
    {
        public string Name { get; set; }
        public double? TotalCost { get; set; }

        public string Message { get; set; }
    }
}
