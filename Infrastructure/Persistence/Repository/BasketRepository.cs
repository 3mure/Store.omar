using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contract;
using Domain.Models;
using StackExchange.Redis;

namespace Persistence.Repository
{
    public class BasketRepository(IConnectionMultiplexer connection ): IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<CustomerBasket?> GetBasketAsync(string Id)
        {
            var redisValue= _database.StringGet(Id);
            if (redisValue.IsNullOrEmpty) return null;
            var Basket = JsonSerializer.Deserialize<CustomerBasket>(redisValue);
            if (Basket is null) return null;
            return  Basket;
             
         
        }
        public Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket, TimeSpan? timeToLive = null)
        {
            var redisvValue = JsonSerializer.Serialize(basket);   

          var flag =   _database.StringSet(basket.Id, redisvValue, TimeSpan.FromDays(30));
            return flag ? GetBasketAsync(basket.Id) : null;
        }
        public Task<bool> DeleteCustomerBasketAsync(string Id) 
        {
           return _database.KeyDeleteAsync(Id);
        }


    }
}
