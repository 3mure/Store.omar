
using System.Threading.Tasks;
using Domain.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Data;
using Persistence.Repository;
using Services;
using Services.Abstractions;
using AssemblyMapping = Services.AssemblyReferance;
 
namespace Store.Omar
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IDbIntializer,DbIntializer>();
            builder.Services.AddScoped<IProductServices, ProductService>();
            builder.Services.AddScoped<IServicesManager, ServicesManager>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(AssemblyMapping).Assembly);


            builder.Services.AddDbContext<StoreDbcontext>(
              options => {
                  //Options=> Options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

                  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")); 
                  
                  }  );

            var app = builder.Build();
            var scope =  app.Services.CreateScope();
           var intializer= scope.ServiceProvider.GetRequiredService<IDbIntializer>();
          await  intializer.InitializeAsync();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
