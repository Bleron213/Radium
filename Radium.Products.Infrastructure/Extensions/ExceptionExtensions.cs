using Microsoft.AspNetCore.Builder;
using Radium.Products.Infrastructure.Middlewares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
