using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Contract;
using Domain.Models;
using Services.Abstractions;
using Shared;

namespace Services
{
    public class ProductService(IUnitOfWork unitOfWork , IMapper mapper ) : IProductServices
    {

        public async Task<IEnumerable<ProductResultDto>> GetAllProductsAsync()
        {
           var products =  await unitOfWork.GetRepository<Product, int>().GetAllAsync();
           var result =  mapper.Map<IEnumerable<ProductResultDto>>(products);
            return result;
        }
        public async Task<ProductResultDto> GetProductByIdAsync(int id)
        {
         var product = await   unitOfWork.GetRepository<Product , int > ().GetByIdAsync(id);
            if(product is null) return null;
            var result = mapper.Map<ProductResultDto>(product);
            return result;
        }
        public async Task<IEnumerable<TypeResultDto>> GetAllTypeSAsync()
        {
          var types = await  unitOfWork.GetRepository<ProductType , int>().GetAllAsync();
            if(types is null) return null;
            var result = mapper.Map<IEnumerable<TypeResultDto>>(types);
            return result;
        }
        public async Task<IEnumerable<BrandResultDto>> GetAllBrandAsync()
        {
          var brands= await  unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            if (brands is null) return null;
            var result = mapper.Map<IEnumerable<BrandResultDto>>(brands);
            return result;
        }

        

       

       
    }
}
