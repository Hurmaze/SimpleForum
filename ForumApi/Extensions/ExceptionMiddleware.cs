using BLL.Validation;
using ForumApi.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
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

                switch (ex)
                {
                    case NotFoundException:
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        _logger.LogWarning(ex.Message);
                        break;

                    case CustomException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        _logger.LogWarning(ex.Message);
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        _logger.LogError("Something went wrong: {mes}", ex);
                        break;
                }

                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = "Internal error occured. We will fix it as fast as we can."
                }.ToString());

            }
        }
    }
}
