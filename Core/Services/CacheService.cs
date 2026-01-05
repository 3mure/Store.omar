using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;
using Domain.Contract;
using Services.Abstractions;

namespace Services
{
    public class CacheService(ICacheRepository cacheRepository) : ICacheService
    {
      

        public async Task<string?> GetCacheValueAsync(string key)
        {
           var value = await cacheRepository.GetAsync(key);
            return value == null ? null : value;
        }

        public async Task SetCacheValueAsync(string Key, object value, TimeSpan duration)
        {
            await cacheRepository.SetAsync(Key, value, duration);
        }
    }
}
