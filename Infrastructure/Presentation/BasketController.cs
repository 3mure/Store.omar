using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController( IServicesManager servicesManager) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketWithId(string Id) 
        {
            var result = await servicesManager.basketServices.GetBasketAsync(Id);
            return  Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasketDto basketDto)
        {
            var result = await servicesManager.basketServices.UpdateCustomerBasketAsync(basketDto);
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            var result = await servicesManager.basketServices.DeleteCustomerBasketAsync(Id);
            return NoContent() ;
        }
    }
}
