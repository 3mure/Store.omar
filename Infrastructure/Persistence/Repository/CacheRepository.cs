using System;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contract;
using StackExchange.Redis;

namespace Persistence.Repository
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<string?> GetAsync(string key)
        {
            var value = await _database.StringGetAsync(key);
            return !string.IsNullOrEmpty(value) ? value : default;
        }

        public async Task SetAsync(string Key, object value, TimeSpan duration)
        {
            var result = JsonSerializer.Serialize(value);
            await _database.StringSetAsync(Key, result, duration);
        }
    }
}
