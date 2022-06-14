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
        private readonly ILogger logger;

        public ExceptionMiddleware(ILogger logger)
        {
            this.logger = logger;
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
                        logger.LogWarning(ex.Message);
                        break;

                    case CustomException:
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        logger.LogWarning(ex.Message);
                        break;

                    default:
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        logger.LogError("Something went wrong: {mes}", ex);
                        break;
                }

                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = ex.Message
                }.ToString());

            }
        }
    }
}
