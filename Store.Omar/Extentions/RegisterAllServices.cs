using Azure;
using Domain.Contract;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using Services;
using Store.Omar.Middleware;

namespace Store.Omar.Extentions
{
    public static class RegisterAllServices
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services , IConfiguration configuration)
        {
            services.Applicationservie();
            services.AddInfrastructureServices(configuration);
            services.RegisterBuldInServices();
            services.ConfigerServices();
            return services;
        }
        private static IServiceCollection RegisterBuldInServices (this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        private static IServiceCollection ConfigerServices(this IServiceCollection services) 
        {
            services.Configure<ApiBehaviorOptions>
                (
                 config => config.InvalidModelStateResponseFactory = (ActionContext) =>
                 {
                     var errors = ActionContext.ModelState.Where(m => m.Value.Errors.Any())
                      .Select(m => new ValidationError()
                      {
                          Field = m.Key,
                          Messages = m.Value.Errors.Select(m => m.ErrorMessage)
                      });
                     var response = new ValidationResponse() { Errors = errors };
                     return new BadRequestObjectResult(response);
                 }

                );
            return services;
        }

        public static WebApplication ConfigerMiddleWare(this WebApplication app)
        {
            app.IntializeDatabaseAsync();
            app.UserDefinded_Middleware();

            app.UseStaticFiles();
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            return app;
        }
        private static async Task<WebApplication> IntializeDatabaseAsync(this WebApplication app) 
        {
            var scope = app.Services.CreateScope();
            var Intializer = scope.ServiceProvider.GetRequiredService<IDbIntializer>();
            await Intializer.InitializeAsync();

            return app;
        }
        private static WebApplication UserDefinded_Middleware(this WebApplication app) 
        {
            app.UseMiddleware<GlobalErrorHandelling>();
            return app;
        }
    }
}
