using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MyApp.Application.Behaviors;
using MyApp.Application.Interfaces;
using MyApp.Application.Mappings;
using MyApp.Core;
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
            service.AddAutoMapper(typeof(StudentProfile));
            service.AddValidatorsFromAssembly(typeof(DependancyInjection).Assembly);
            service.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return service;
        }
    }
}
