using ForumApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Services.Validation.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ForumApi.Extensions
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;

        public ExceptionMiddleware(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                var error = new ErrorDetails();

                error.ErrorMessage = ex.Message;
                error.Source = ex.Source;

                switch (ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    case CustomException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        error.ErrorMessage = "Internal error occured. We will fix it as fast as we can.";
                        break;
                }

                var statusCode = (HttpStatusCode)context.Response.StatusCode;

                switch (statusCode)
                {
                    case HttpStatusCode.NotFound:
                        _logger.LogWarning(ex.Message);
                        break;

                    default:
                        _logger.LogError(ex.Message);
                        break;
                }

                await context.Response.WriteAsync(error.ToString());
            }
        }
    }
}
