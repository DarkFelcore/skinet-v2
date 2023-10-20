using Microsoft.AspNetCore.Mvc.Infrastructure;
using SkinetV2.Api.Common;
using SkinetV2.Api.Common.Errors;

namespace SkinetV2.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });
            services.AddSingleton<ProblemDetailsFactory, SkinetProblemDetailFactory>();
            services.AddMappings();
            services.AddCors(options => options.AddPolicy(name: "CorsPolicy",
                policy =>
                {
                    policy.WithOrigins("https://localhost:4200").AllowAnyMethod().AllowAnyHeader();
                }
            ));
            return services;
        }
    }
}