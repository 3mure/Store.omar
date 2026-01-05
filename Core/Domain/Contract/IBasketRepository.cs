using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Contract
{
    public interface IBasketRepository
    {

        Task<CustomerBasket?> GetBasketAsync(string Id);
        Task<CustomerBasket?> UpdateCustomerBasketAsync(CustomerBasket basket ,TimeSpan? timeToLive=null );
        Task<bool> DeleteCustomerBasketAsync(string Id);
    }
}
