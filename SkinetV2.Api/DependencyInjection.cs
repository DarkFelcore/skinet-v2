using Microsoft.AspNetCore.Mvc.Infrastructure;
using SkinetV2.Api.Common;
using SkinetV2.Api.Common.Errors;

namespace SkinetV2.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, SkinetProblemDetailFactory>();
            services.AddMappings();
            return services;
        }
    }
}