using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Domain.Contract;
using Domain.Models;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence
{
    public class DbIntializer : IDbIntializer
    {
        private readonly StoreDbcontext _storeDbcontext;
        private readonly StoreIdentityDbContext _IdentityContexct;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbIntializer(StoreDbcontext storeDbcontext,
            StoreIdentityDbContext IdentityContext,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager

            )
        {
           _storeDbcontext = storeDbcontext;
           _IdentityContexct = IdentityContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task IdentityInitializeAsync()
        {
            //migrations if they are not applied
            if (_IdentityContexct.Database.GetPendingMigrations().Any())
            {
                await _IdentityContexct.Database.MigrateAsync();
            }
            //seeding
            if (!_roleManager.Roles.Any()) 
            {
               await _roleManager.CreateAsync ( new IdentityRole {Name="Admin" });
                await _roleManager.CreateAsync(new IdentityRole { Name = "SuberAdmin" });

            }

            if (!_userManager.Users.Any())
            {
                var SuberAdmin = new AppUser
                {
                    DisplayName = "suberAdmin",
                    Email = "SuberAdmin@gmail.com",
                    UserName = "SuberAdmin",
                    Address = new Address
                    {
                        FirstName = "Omar",
                        LastName = "Ali",
                        Street = "123 Main St",
                        City = "Cairo",
                        State = "Cairo"
                    },
                    PhoneNumber = "01123456789"

                };
                var Admin = new AppUser
                {
                    DisplayName = "Admin",
                    Email = "Admin@gmail.com",
                    UserName = "Admin",
                    Address = new Address
                    {
                        FirstName = "Ahmed",
                        LastName = "Ali",
                        Street = "123 Main St",
                        City = "Cairo",
                        State = "Cairo"
                    },
                    PhoneNumber = "01123456789"

                };
                _userManager.CreateAsync(SuberAdmin, "P@ssW0rd").Wait();
                _userManager.CreateAsync(Admin, "P@ssW0rd").Wait();

                await _userManager.AddToRoleAsync(SuberAdmin, "SuberAdmin");
                await _userManager.AddToRoleAsync(Admin, "Admin");


            }
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