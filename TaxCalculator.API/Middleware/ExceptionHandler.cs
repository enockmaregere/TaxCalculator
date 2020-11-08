using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

using Serilog;

using System;
using System.IO;
using System.Net;

using TaxCalculator.RESTAPI.Exceptions;
using TaxCalculator.RESTAPI.Models;

namespace TaxCalculator.RESTAPI.Middleware
{
    public static class ExceptionHandler
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) =>
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        var baseException = contextFeature.Error.GetBaseException();

                        if (baseException is CustomException exception)
                        {
                            Log.Error("Exception: Username: {Username} Error: {Error} RequestBody: {RequestBody} Claims: {Claims}",
                            new object[]
                            {
                                contextFeature.Error.Message,
                            });

                            context.Response.StatusCode = exception.StatusCode;

                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = exception.StatusCode,
                                Message = exception.Message
                            }.ToString()).ConfigureAwait(true);
                        }
                        else
                        {
                            Log.Error("An exception occured",
                            new object[]
                            {
                                contextFeature.Error,
                                GetRequestBodyContents(context.Request),
                            });

                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = (int)HttpStatusCode.InternalServerError,
                                Message = "Unexpected error has occured, please contact the administrator"
                            }.ToString()).ConfigureAwait(true);
                        }
                    }
                });
            });

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1031:Do not catch general exception types", Justification = "<Pending>")]
        public static string GetRequestBodyContents(HttpRequest Request)
        {
            if (Request == null)
                return string.Empty;

            try
            {
                var bodyText = "";
                using (var bodyStream = new StreamReader(Request.Body))
                {
                    bodyStream.BaseStream.Seek(0, SeekOrigin.Begin);
                    bodyText = bodyStream.ReadToEnd();
                }

                return bodyText;
            }
            catch (Exception)
            {
                //Ignore exception and return empty string.
                //Do not want to throw exception in global API Exception handler
                return string.Empty;
            }
        }
    }
}

