using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Radium.Shared.Utils.Errors;
using Radium.Shared.Utils.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
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
            HttpResponse response = context.Response;

            response.ContentType = "application/json";
            ErrorDetails errorDetails = new()
            {
                Succeeded = false
            };

            if (exception is AppException)
            {
                var appException = exception as AppException;
                errorDetails.ErrorMessage = appException.Error.ErrorKey;
                errorDetails.ErrorExceptionMessage = appException.Error.Message;
            }
            else if (exception is ValidationException)
            {
                var validationException = exception as ValidationException;
                errorDetails.ErrorMessage = "validation_failed";
                errorDetails.ErrorExceptionMessage = validationException.Message;
                errorDetails.Errors = validationException.Errors.Select(x => new Error { ErrorMessage = x.PropertyName, ErrorExceptionMessage = x.ErrorMessage }).ToList();
            }
            else if (exception is BadHttpRequestException)
            {
                errorDetails.ErrorMessage = "bad_request";
                errorDetails.ErrorExceptionMessage = exception.Message;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else 
            {
                errorDetails.ErrorMessage = "something_went_wrong";
                errorDetails.ErrorExceptionMessage = "Something went wrong";
                response.StatusCode = (int)HttpStatusCode.InternalServerError;

                _logger.LogWarning("Unhandled error: {ex}", exception.ToString());
            }

            if (_hostingEnv.IsDevelopment() && (string.IsNullOrEmpty(errorDetails.ErrorMessage) || string.IsNullOrEmpty(errorDetails.ErrorExceptionMessage)))
            {
                errorDetails.ErrorMessage = "something_went_wrong";
                errorDetails.ErrorExceptionMessage = exception.ToString();
            }
            
            string responseText = System.Text.Json.JsonSerializer.Serialize(errorDetails, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            });

            await response.WriteAsync(responseText);
        }
    }
}
