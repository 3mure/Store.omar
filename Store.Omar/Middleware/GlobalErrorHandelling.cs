using Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared.ErrorModels;

namespace Store.Omar.Middleware
{
    public class GlobalErrorHandelling
    {
        private readonly RequestDelegate _next;
       private readonly ILogger<GlobalErrorHandelling> _logger;
        public GlobalErrorHandelling(RequestDelegate next,ILogger<GlobalErrorHandelling> logger)
        {
         _next = next;
         _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
                if(context.Response.StatusCode== StatusCodes.Status404NotFound)
                {
                    
                    context.Response.ContentType = "application/json";
                    var response = new ErrorDetails
                    {
                        StatusCode = StatusCodes.Status404NotFound,
                        Message = $"The End Point {context.Request.Path} you are looking for was not found."
                    };
                    context.Response.WriteAsJsonAsync(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
               // context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new ErrorDetails
                {
                    //StatusCode = StatusCodes.Status500InternalServerError,
                    Message = ex.Message
                };
                response.StatusCode = ex switch
                {
                    NotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };
                context.Response.StatusCode = response.StatusCode;

                await context.Response.WriteAsJsonAsync(response);


            }
        }
    }
}
