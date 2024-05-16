﻿using EksiSozlukClone.Common.Infastructure.Exceptions;
using EksiSozlukClone.Common.Infastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace EksiSozlukClone.Api.WebApi.Infastructure.ActionFilters.Extensions;

public static class ApplicationBuilderExtension
{
    public static IApplicationBuilder ConfigureExceptonHandler(this IApplicationBuilder app,
        bool includeExceptionDetails = false,
        bool useDefaultHandlingResponse = true,
        Func<HttpContext, Exception, Task> handleException = null)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(context =>
            {
                var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();

                if (!useDefaultHandlingResponse && handleException == null)
                {
                    throw new ArgumentNullException(nameof(handleException),
                    $"{nameof(handleException)} cannot be null when {nameof(useDefaultHandlingResponse)} is false");
                }

                if (!useDefaultHandlingResponse && handleException != null)
                {
                    return handleException(context, exceptionObject.Error);
                }
                return DefaultHandlerException(context, exceptionObject.Error, includeExceptionDetails);

            });
        });
        
    return app;
    }

    private static async Task DefaultHandlerException(HttpContext context, Exception exception, bool includeExceptionDetails)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "Internal server error occured!";

        if(exception is UnauthorizedAccessException)
        {
            statusCode=HttpStatusCode.Unauthorized;
        }
        if(exception is DatabaseValidationException)
        {
            statusCode = HttpStatusCode.BadRequest;
            var validationResponse = new ValidationResponseModel(exception.Message);
            await WriteResponse(context, statusCode, validationResponse);
            return;
        }

        var res = new
        {
            HttpStatusCode = (int)statusCode,
            Detail = includeExceptionDetails ? exception.ToString() : message
        };
        await WriteResponse(context, statusCode, res);
    }

    private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObj)
    {
        context.Response.ContentType="application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsJsonAsync(responseObj);
    }
       
}
