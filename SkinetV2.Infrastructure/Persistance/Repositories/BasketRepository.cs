using System.Text.Json;
using SkinetV2.Application.common.Interfaces;
using SkinetV2.Domain.Baskets;
using StackExchange.Redis;

namespace SkinetV2.Infrastructure.Persistance.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        public BasketRepository(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return await _database.KeyDeleteAsync(basketId);
        }

        public async Task<CustomerBasket?> GetBasketAsync(string basketId)
        {
            var data = await _database.StringGetAsync(basketId);
            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data!); 
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket basket)
        {
            // In case of update, override the existing basket with the new basket
            var created = await _database.StringSetAsync(basket.BasketId, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));

            return !created ? null : await GetBasketAsync(basket.BasketId);
        }
    }
}