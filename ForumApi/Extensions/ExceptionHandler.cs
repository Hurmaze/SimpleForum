using BLL.Validation;
using ForumApi.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ForumApi.Extensions
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if(contextFeature != null)
                    {
                        logger.LogError("Something went wrong: {mes}", contextFeature.Error);

                        switch (contextFeature.Error)
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

                        if(contextFeature.Error is CustomException)
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }

                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
