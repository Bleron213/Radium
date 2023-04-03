using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Radium.Shared.Utils.Errors;
using Radium.Shared.Utils.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Radium.Products.Infrastructure.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostingEnv;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment hostingEnv)
        {
            _next = next;
            _logger = logger;
            _hostingEnv = hostingEnv;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Error in Custom Middleware");

            var response = new ErrorDetails();

            if (_hostingEnv.IsDevelopment())
            {
                response.ErrorMessage = "Exception";
                response.ErrorExceptionMessage = exception.ToString();
            }

            context.Response.ContentType = "application/json";

            if (exception is CustomError)
            {
                var customError = exception as CustomError;

                response.Message = "Custom Error ocurred";
                response.Errors.AddRange(customError.SerializeErrors());
                context.Response.StatusCode = (int)customError.StatusCode;
                await context.Response.WriteAsync(response.ToString());
                return;
            }

            if (exception is BadHttpRequestException)
            {
                response.Message = "A BadRequest was received";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await context.Response.WriteAsync(response.ToString());
                return;
            }

            response.Message = "Unexpected error ocurred";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await context.Response.WriteAsync(response.ToString());

        }
    }
}
