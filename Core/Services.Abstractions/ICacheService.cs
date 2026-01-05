using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICacheService
    {
        Task<string?> GetCacheValueAsync(string key);
        
        Task SetCacheValueAsync(string Key, object value, TimeSpan duration);

    }
}
