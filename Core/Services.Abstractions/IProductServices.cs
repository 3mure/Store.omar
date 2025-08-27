using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace Services.Abstractions
{
    public interface IProductServices
    {
        Task<Pagination_Response<ProductResultDto>> GetAllProductsAsync(ProductSpacificationPramater pramater);
        Task<ProductResultDto> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandResultDto>> GetAllBrandAsync();
        Task<IEnumerable<TypeResultDto>> GetAllTypeSAsync();
    }
}
