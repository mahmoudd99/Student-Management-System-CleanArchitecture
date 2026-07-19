using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Core
{
    public static class DependancyInjection
    {

        public static IServiceCollection AddCoreDI(this IServiceCollection service)
        {
            return service;
        }
    }
}
