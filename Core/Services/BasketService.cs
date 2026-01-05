using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contract;
using Domain.Exceptions;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class BasketService(IBasketRepository _repository , IMapper mapper) : IBasketServices
    {
        public async Task<CustomerBasketDto> GetBasketAsync(string Id)
        {
           var basket = await  _repository.GetBasketAsync(Id);
            if (basket is null) throw new BasketNotFoundException(Id);
           var basketDto=  mapper.Map<CustomerBasketDto>(basket);
            return basketDto;


        }

       
        public async Task<CustomerBasketDto> UpdateCustomerBasketAsync(CustomerBasketDto basketDto)
        {
            var basket = mapper.Map<CustomerBasket>(basketDto);


            var UpdatedBasked = await _repository.UpdateCustomerBasketAsync(basket);
            var result = mapper.Map<CustomerBasketDto>(UpdatedBasked);
            if (result is null) throw new BasketBadRequestException();
            return  result;
        }
        public async Task<bool> DeleteCustomerBasketAsync(string Id)
        {
            var flag = await _repository.DeleteCustomerBasketAsync(Id);
            if (!flag) throw new BasketBadRequestException();
            return flag;

        }

    }
}
