using DomainService.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingEntity.Context;
using ParkingApplication.Implementation;
using ParkingApplication.Interface;
using DomainService.Implementation;

namespace DIConfig
{
    public static class DIConfig
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            return services.AddScoped<IParkingCalculatorService, ParkingCalclatorService>()
                    ;
        }

        public static IServiceCollection AddDomainService(this IServiceCollection services)
        {
            return services.AddScoped<IParkingRateService, ParkingRateService>()
                    ;
        }
        public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
        {
            return

                services.AddDbContext<ParkingRateContext>(opts => opts.
                UseSqlServer(configuration.GetConnectionString("ParkingRateConnection")))
                .AddScoped<IParkingRateContext, ParkingRateContext>();
            ;
        }
    }
}
