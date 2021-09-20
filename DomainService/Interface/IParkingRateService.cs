using ParkingEntity.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomainService.Interface
{
    public interface IParkingRateService
    {
        Task<List<ParkingRate>> GetAllParkingRates();
    }
}
