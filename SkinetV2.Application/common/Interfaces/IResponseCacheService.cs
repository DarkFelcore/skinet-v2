namespace SkinetV2.Application.common.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheReponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string?> GetCachedResponseAsync(string cacheKey);
    }
}