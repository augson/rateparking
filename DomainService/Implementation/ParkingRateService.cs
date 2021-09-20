using DomainService.Interface;
using Microsoft.EntityFrameworkCore;
using ParkingEntity.Context;
using ParkingEntity.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainService.Implementation
{
    /// <summary>
    /// Parking Domain Service to manage Parking Rate and its Rules and definitions
    /// </summary>
    public class ParkingRateService : IParkingRateService
    {
        private readonly IParkingRateContext _parkingRateContext;
        public ParkingRateService(IParkingRateContext parkingRateContext)
        {
            _parkingRateContext = parkingRateContext;
        }
        public async Task<List<ParkingRate>> GetAllParkingRates()
        {
           return  await  _parkingRateContext.ParkingRate.Where(s=>s.IsValid).Include(s => s.ParkingRateRule).
                ThenInclude(def => def.ParkingRateRuleDefinition).ToListAsync();
        }
    }
}
