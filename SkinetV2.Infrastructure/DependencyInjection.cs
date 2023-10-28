using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Infrastructure.Authentication;
using SkinetV2.Infrastructure.Persistance;
using SkinetV2.Infrastructure.Persistance.Repositories;
using SkinetV2.Infrastructure.Services;
using StackExchange.Redis;

namespace SkinetV2.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddAuth(configuration)
                .AddPersistance(configuration);
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
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
            services.AddSingleton<IConnectionMultiplexer>(x =>
            {
                var config = ConfigurationOptions.Parse(configuration.GetConnectionString("Redis")!, true);
                return ConnectionMultiplexer.Connect(config);
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,

                    ValidateIssuer = true,
                    ValidIssuer = jwtSettings.Issuer,

                    ValidateAudience = true,
                    ValidAudience = jwtSettings.Audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret)
                    )
                });

            return services;
        }
    }
}