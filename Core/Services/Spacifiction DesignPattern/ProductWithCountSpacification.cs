using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Spacifiction_DesignPattern
{
    public class ProductWithCountSpacification: BaseSpacifiction<Product, int>
    {
        public ProductWithCountSpacification(ProductSpacificationPramater pramater) : base
            (
             p =>
             (string.IsNullOrEmpty(pramater.Search) || p.Name.ToLower().Contains(pramater.Search.ToLower()) )&&
            (!  pramater.BrandId.HasValue || p.BrandId == pramater.BrandId)
            && (!pramater.TypeId.HasValue || p.TypeId == pramater.TypeId)
            )
        {
            
        }
    }
}
