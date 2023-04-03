using Microsoft.AspNetCore.Builder;
using Radium.Products.Infrastructure.Middlewares;

namespace Radium.Products.Infrastructure.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
