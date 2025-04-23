using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contract;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbIntializer : IDbIntializer
    {
        private readonly StoreDbcontext _storeDbcontext;

        public DbIntializer(StoreDbcontext storeDbcontext)
        {
           _storeDbcontext = storeDbcontext;
        }
        public async Task InitializeAsync()
        {
            if (_storeDbcontext.Database.GetPendingMigrations().Any()) 
            {
               await _storeDbcontext.Database.MigrateAsync();
            }
            //Data Seeding
            if (!_storeDbcontext.ProductBrands.Any()) 
            {
                //read Json file
             var BrandData=  await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\brands.json");
                //convert json to object 
              var brand=  JsonSerializer.Deserialize<List<ProductBrand>>(BrandData);
                //add to database
                if (brand is not null && brand.Any()) 
                {
                    await _storeDbcontext.ProductBrands.AddRangeAsync(brand);
                    await _storeDbcontext.SaveChangesAsync();


                }

            }
            if (!_storeDbcontext.ProductTypes.Any())
            {
              var DataType= await  File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\types.json");
                //convert jason to object
               var type = JsonSerializer.Deserialize<List<ProductType>>(DataType);
                //add to database
                if (type is not null && type.Any())
                { 
                    await _storeDbcontext.ProductTypes.AddRangeAsync(type);
                    await _storeDbcontext.SaveChangesAsync();
                }
            }
            if (!_storeDbcontext.Products.Any())
            {
                var DataProducts = await File.ReadAllTextAsync(@"..\Infrastructure\Persistence\Seeding\products.json");
                //convert jason to object
                var products = JsonSerializer.Deserialize<List<Product>>(DataProducts);
                //add to database
                if (products is not null && products.Any())
                {
                    await _storeDbcontext.Products.AddRangeAsync(products);
                    await _storeDbcontext.SaveChangesAsync();
                }
            }


        }
    }

}//C:\visual studio\Store.Omar\Infrastructure\Persistence\Seeding\products.json