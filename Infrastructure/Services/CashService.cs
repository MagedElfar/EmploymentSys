using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class CashService : ICacheService
    {
        private readonly IDatabase database;
        private readonly TimeSpan defaultExpiration;

        public CashService(IConnectionMultiplexer redis, TimeSpan? defaultExpiration = null)
        {
            database = redis.GetDatabase();
            this.defaultExpiration = defaultExpiration ?? TimeSpan.FromHours(1); // Default to 1 hour
        }

        public async Task<T> GetData<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            var data = await database.StringGetAsync(key);
            return data.IsNullOrEmpty ? default : JsonSerializer.Deserialize<T>(data);
        }

        public async Task<bool> RemoveData(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            return await database.KeyDeleteAsync(key);
        }

        public async Task<bool> SetData<T>(string key, T value, TimeSpan? expirationTime = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty", nameof(key));

            var expiration = expirationTime ?? defaultExpiration;
            var serializedValue = JsonSerializer.Serialize(value);

            return await database.StringSetAsync(key, serializedValue, expiration);
        }
    }
}
