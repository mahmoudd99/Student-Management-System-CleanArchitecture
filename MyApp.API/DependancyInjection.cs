using MyApp.Application;
using MyApp.Core;
using MyApp.Infrastructure;

namespace MyApp.API
{
    public  static class DependancyInjection
    {

        public static IServiceCollection AddApiDI(this IServiceCollection service)
        {
            service.AddInfrastructureDI().AddApplicationDI();
            return service;
        }
    }
}
