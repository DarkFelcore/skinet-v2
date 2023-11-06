
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SkinetV2.Application.common.Interfaces;

namespace SkinetV2.Api.Common.Helpers
{
    // This attribute runs before the actual code of the controller
    public class CachedAttribute : Attribute, IAsyncActionFilter
    {
        private readonly int _timeToLiveSeconds;
        public CachedAttribute(int timeToLiveSeconds)
        {
            _timeToLiveSeconds = timeToLiveSeconds;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var cachedService = context.HttpContext.RequestServices.GetRequiredService<IResponseCacheService>();

            var cacheKey = GenerateCacheKeyFromRequest(context.HttpContext.Request);

            var cachedResponse = await cachedService.GetCachedResponseAsync(cacheKey);

            // If there is something in cache, then we send the response ourselfs.
            if(!string.IsNullOrEmpty(cachedResponse))
            {
                var contentResult = new ContentResult
                {
                    Content = cachedResponse,
                    ContentType = "application/json",
                    StatusCode = 200
                };

                context.Result = contentResult;

                return;
            }

            // Move to the controller
            var executedContext = await next();

            // When the controller access the database, set this requested data in cache so that it is available in cache for next request
            if(executedContext.Result is OkObjectResult okObjectResult)
            {
                await cachedService.CacheReponseAsync(cacheKey, okObjectResult.Value!, TimeSpan.FromSeconds(_timeToLiveSeconds));
            }
        }

        private string GenerateCacheKeyFromRequest(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();

            keyBuilder.Append($"{request.Path}");

            foreach(var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.AppendLine($"|{key}-{value}");
            }

            return keyBuilder.ToString();
        }
    }
}