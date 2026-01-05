using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contract
{
    public interface ICacheRepository
    {
        //Task GetAsync(string key);
        //Task SetAsync(string key, object value, TimeSpan? expiration = null);
        //Task RemoveAsync(string key);
        Task<string?> GetAsync(string key);
       
        Task SetAsync(string Key, object value, TimeSpan duration);

    }
}
