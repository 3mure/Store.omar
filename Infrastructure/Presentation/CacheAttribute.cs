using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstractions;

namespace Presentation
{
    public class CacheAttribute(int DurationInSec ) : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
           var CacheService=  context.HttpContext.RequestServices.GetRequiredService<IServicesManager>().cacheService;
            string key = GenrateCacheKey(context.HttpContext.Request);
            var result = await CacheService.GetCacheValueAsync(key);

            if (!string.IsNullOrEmpty(result)) 
            {
                context.Result = new ContentResult()
                { 
                 StatusCode = StatusCodes.Status200OK,
                 ContentType= "application/json",
                 Content = result
                };
                return;
            }
            var ActionResult = await next.Invoke();
            if (ActionResult.Result is OkObjectResult okObjectResult) 
            {
                CacheService.SetCacheValueAsync(key, okObjectResult.Value, TimeSpan.FromSeconds(DurationInSec));
            }
        }
        private string GenrateCacheKey(HttpRequest request ) 
        {
            var CacheKey = new StringBuilder();
            CacheKey.Append($"{request.Path}");
            foreach (var query in request.Query) 
            {
                CacheKey.Append($"|{query.Key}-{query.Value}");
            }
            return CacheKey.ToString();
        
        }
    }
}
