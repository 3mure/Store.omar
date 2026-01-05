using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IBasketServices
    {

        Task<CustomerBasketDto> GetBasketAsync(string Id);
        Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto);
        Task<bool> DeleteCustomerBasketAsync(string Id);
    }
}
