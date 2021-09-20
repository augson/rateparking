using ParkingEntity.Dto;
using ParkingApplication.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using DomainService.Interface;
using ParkingApplicationService.Common;
using System.Threading.Tasks;
using ParkingEntity.Entity;
using ParkingEntity.ApiDto.Response;
using System.Linq;
using ParkingEntity.ApiDto.Messages;
using ParkingEntity.ApiDto.Request;
using Infrastructure.Util;

namespace ParkingApplication.Implementation
{
   
    public class ParkingCalclatorService : IParkingCalculatorService
    {
        private readonly IParkingRateService _parkingRateService;
        public ParkingCalclatorService(IParkingRateService  parkingRateService)
        {
            _parkingRateService = parkingRateService;
        }

        public  List<ParkingRateRule> GetApplicatbleParkingRates(DateTime startDateTime, DateTime endDateTime, List<ParkingRate> parkingRates)
        {
            var customDate = new CustomDate(startDateTime, endDateTime);
           

            var matchingRates =  customDate.GetMatchingParkingRate(parkingRates);
            return matchingRates;
        }

        public async Task<ParkingRateResponse> GetMinimumParkingRate(ParkingRateRequest parkingRateRequest)
        {
            try
            {
                var startDate = parkingRateRequest.EntryDate.GetValidDateTime(true);
                var exitDate = parkingRateRequest.ExitDate.GetValidDateTime(true);
                if (IsValidParkingRequest(startDate, exitDate))
                {
                    var parkingRates = await _parkingRateService.GetAllParkingRates();
                    var customDate = new CustomDate(startDate.Value, exitDate.Value);
                    var matchingRates = customDate.GetMatchingParkingRate(parkingRates);

                    if (matchingRates.Count > 0)
                    {
                        var minimumRate = matchingRates.OrderBy(mr => mr.Rate).First();
                        return new ParkingRateResponse
                        {
                            Message = SuccessMessage.MATCHING_RATE_FOUND,
                            Name = minimumRate.ParkingRate.Name,
                            TotalCost = minimumRate.Rate
                        };
                    }
                    return new ParkingRateResponse
                    {
                        Message = SuccessMessage.NO_RATE_FOUND,
                        Name = string.Empty,

                    }; ;
                }
                return new ParkingRateResponse
                {
                    Message = ErrorMessages.INVALID_ENTRY,
                    Name = string.Empty,

                };
            }
            catch
            {
                return new ParkingRateResponse
                {
                    Message = ErrorMessages.INVALID_ENTRY,
                    Name = string.Empty,

                };
            }
        }

        #region Helper Functions
        private bool IsValidParkingRequest(DateTime? startDate,DateTime? exitDate)
        {
            try
            {
                return (startDate.HasValue && exitDate.HasValue && startDate.Value != DateTime.MinValue &&
                    startDate.Value != DateTime.MaxValue &&
                    exitDate.Value != DateTime.MinValue && exitDate.Value != DateTime.MaxValue&&
                    startDate.Value < exitDate.Value);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
