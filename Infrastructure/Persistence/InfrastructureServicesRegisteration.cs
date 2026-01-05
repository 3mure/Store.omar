using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Data;
using Persistence.Repository;
using StackExchange.Redis;

namespace Persistence
{
    public static  class InfrastructureServicesRegisteration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IDbIntializer, DbIntializer>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();
            services.AddSingleton<IConnectionMultiplexer>((ServiceProvider) =>
            {
               return ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);

            });
            services.AddDbContext<StoreIdentityDbContext>(Options=>
             Options.UseSqlServer(configuration.GetConnectionString(""))
                
                );
            services.AddDbContext<StoreDbcontext>(
           options => {
               //Options=> Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

               options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

           });
          
            return services;
        }
    }
}
