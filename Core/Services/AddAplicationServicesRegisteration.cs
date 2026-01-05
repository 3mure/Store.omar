using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Services
{
    public static  class AddAplicationServicesRegisteration
    {
        public static IServiceCollection Applicationservie(this IServiceCollection services)
        {
           services.AddScoped<IProductServices, ProductService>();
             services.AddScoped<IServicesManager, ServicesManager>();

             services.AddAutoMapper(typeof(AssemblyReferance).Assembly);


            return services;
        }
    }
}
