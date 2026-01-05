using System.Threading.Tasks;
using Domain.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Data;
using Persistence.Repository;
using Services;
using Services.Abstractions;
using Store.Omar.Extentions;
using AssemblyMapping = Services.AssemblyReferance;
using Microsoft.AspNetCore.Identity;
using Domain.Models.Identity;

namespace Store.Omar
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.RegisterServices(builder.Configuration);
            builder.Services.AddIdentity<AppUser, IdentityRole>()
       .AddEntityFrameworkStores<StoreDbcontext>()
       .AddDefaultTokenProviders();

            var app = builder.Build();
            app.ConfigerMiddleWare();

            app.Run();
        }
    }
}
