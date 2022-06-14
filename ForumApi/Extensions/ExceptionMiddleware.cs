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
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {

                context.Response.ContentType = "application/json";

                logger.LogError("Something went wrong: {mes}", ex);

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
