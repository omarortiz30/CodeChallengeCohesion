using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using System.Reflection;


namespace ServiceRequest.Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
