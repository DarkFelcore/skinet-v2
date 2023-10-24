using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Infrastructure.Persistance;
using SkinetV2.Infrastructure.Persistance.Repositories;
using StackExchange.Redis;

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
            // SQL Server
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

            // Redis Local Connection (Package: StackExchange.Redis)
            services.AddSingleton<IConnectionMultiplexer>(x => {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!, true);
                return ConnectionMultiplexer.Connect(config);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            return services;
        }
    }
}