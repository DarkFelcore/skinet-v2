using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Infrastructure.Persistance;
using SkinetV2.Infrastructure.Persistance.Repositories;

namespace SkinetV2.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPersistance(configuration);
            return services;
        }

        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), action =>
                {
                    // Throws timeout exception whenever a request takes longer than 30 seconds.
                    action.CommandTimeout(30);
                });
                // Do not enable this in production
                //options.EnableDetailedErrors(true);
                //options.EnableSensitiveDataLogging(true);
            });
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            return services;
        }
    }
}