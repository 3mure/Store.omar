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
    public class ProductsController(IServicesManager servicesManager ) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync([FromQuery] ProductSpacificationPramater paramters  )
        {
           var result= await servicesManager.productServices.GetAllProductsAsync(paramters );
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetproductbyId(int id)
        {
            var result = await servicesManager.productServices.GetProductByIdAsync(id);

            if (result is null) return NotFound();
            return Ok(result);
        }

       
        [HttpGet ("brands")]
        public async Task<IActionResult> GetAllBrand()
        {
            var result = await servicesManager.productServices.GetAllBrandAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }
        [HttpGet ("typs")]
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await servicesManager.productServices.GetAllTypeSAsync();
            if (result is null) return BadRequest();
            return Ok(result);
        }





    }
}
