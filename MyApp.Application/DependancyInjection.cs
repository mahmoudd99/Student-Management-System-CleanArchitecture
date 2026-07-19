using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Application
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddApplicationDI(this IServiceCollection service)
        {
           
            service.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependancyInjection).Assembly));


            return service;
        }
    }
}
