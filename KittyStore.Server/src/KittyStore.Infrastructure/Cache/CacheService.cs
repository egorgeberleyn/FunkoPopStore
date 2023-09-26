using KittyStore.Application.Common.Interfaces.Cache;
using KittyStore.Infrastructure.Exceptions;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace KittyStore.Infrastructure.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IDatabaseAsync _redisDatabase;

        public CacheService(IConnectionMultiplexer multiplexer)
        {
            _redisDatabase = multiplexer.GetDatabase();
        }

        public async Task<T?> GetDataAsync<T>(string key)
        {
            var value = await _redisDatabase.StringGetAsync(key);
            return !string.IsNullOrEmpty(value)
                ? JsonConvert.DeserializeObject<T>(value)
                : default;
        }

        public async Task SetDataAsync<T>(string? key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            var isSuccess = await _redisDatabase.StringSetAsync(key,
                JsonConvert.SerializeObject(value), expiryTime);
            if (!isSuccess)
                throw new StringSetException("String installation error");
        }

        public async Task<bool> RemoveDataAsync(string key)
        {
            var isKeyExist = await _redisDatabase.KeyExistsAsync(key);

            return isKeyExist &&
                   await _redisDatabase.KeyDeleteAsync(key);
        }
    }
}