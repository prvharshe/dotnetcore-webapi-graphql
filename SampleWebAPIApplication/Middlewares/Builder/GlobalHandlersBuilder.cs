using System;
using Microsoft.AspNetCore.Builder;

namespace SampleWebAPIApplication.Middlewares.Builder
{
    public static class MiddlewareExtensions
    {
        /// <summary>
        /// Adds a middleware to the pipeline that will globaly catch exceptions.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<GlobalInternalErrorHandlerMiddleware>();
        }

        /// <summary>
        /// Adds a middleware to the pipeline that will globaly log handled and unhandled exceptions.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseGlobalLoggerHandler(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<GlobalLoggerMiddleware>();
        }
    }
}
