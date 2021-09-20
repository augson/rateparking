using ParkingEntity.ApiDto.Request;
using ParkingEntity.ApiDto.Response;
using ParkingEntity.Dto;
using ParkingEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParkingApplication.Interface
{
    public interface IParkingCalculatorService
    {
        Task<ParkingRateResponse> GetMinimumParkingRate(ParkingRateRequest parkingRateRequest);
        List<ParkingRateRule> GetApplicatbleParkingRates(DateTime startDateTime, DateTime endDateTime,
            List<ParkingRate> parkingRates);
    }
}
