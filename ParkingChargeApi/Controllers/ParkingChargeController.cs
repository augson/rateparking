using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParkingApplication.Interface;
using ParkingEntity.Context;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Util;
using ParkingEntity.ApiDto.Request;

namespace ParkingChargeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingChargeController : ControllerBase
    {
        private readonly IParkingCalculatorService _parkingCalculatorService;
       
        public ParkingChargeController(IParkingCalculatorService parkingCalculatorService)
        {
            _parkingCalculatorService = parkingCalculatorService;
            
        }

        /// <summary>
        /// Service for retrieving Minimum parking charge.
        /// </summary>
        /// <param name="parkingRateRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetParkingCharge")]
        public async  Task<ActionResult> GetParkingCharge([FromBody] ParkingRateRequest parkingRateRequest)
        {

            var result =  await _parkingCalculatorService.GetMinimumParkingRate(parkingRateRequest);
            return Ok(result);
        }

       
    }
}
