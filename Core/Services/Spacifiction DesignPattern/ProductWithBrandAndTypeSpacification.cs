using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared;

namespace Services.Spacifiction_DesignPattern
{
    public class ProductWithBrandAndTypeSpacification :BaseSpacifiction<Product,int>
    {
        public ProductWithBrandAndTypeSpacification(ProductSpacificationPramater pramater) :base(
            p=>
            (string.IsNullOrEmpty(pramater.Search) || p.Name.ToLower().Contains(pramater.Search.ToLower()))
            &&
            (!pramater. BrandId.HasValue||p.BrandId==pramater.BrandId)
            &&(!pramater.TypeId.HasValue || p.TypeId == pramater.TypeId)
            )
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            applyorderby(pramater.Sort );
            ApplyPagination(pramater.PageIndex , pramater.PageSize);
        }
        public ProductWithBrandAndTypeSpacification(int id ):base(p=>p.Id==id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            
        }

        private void applyorderby(string? sort) 
        {
            if (!string.IsNullOrEmpty(sort)) 
            {
                switch (sort.ToLower()) {

                    case "priceasc":
                        AddOrderBy(p=>p.Price);
                        break;
                    case "pricedesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    
                    case "namedesc":
                        AddOrderByDescending(p => p.Name);
                        break;

                     default:
                    
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(p => p.Name);
            };

            
        }
       

    }
}
