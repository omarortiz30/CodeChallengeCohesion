using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceRequest.Application;

namespace ServiceRequest.Persistence
{
    public static class DI
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("ServiceRequestDB"));

            services.AddScoped<IAppDbContext>(provider => provider.GetService<AppDbContext>());

            return services;
        }
    }
}
