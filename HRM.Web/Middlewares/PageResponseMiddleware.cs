using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRM.Web.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class PageResponseMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public PageResponseMiddleware(RequestDelegate next, ILoggerFactory logFactory)
        {
            _next = next;
            _logger = logFactory.CreateLogger("PageResponseMiddleware");
        }

        public Task Invoke(HttpContext httpContext)
        {
            var watch = new Stopwatch();
            watch.Start();
            httpContext.Response.OnStarting(() => {
                // Stop the timer information and calculate the time   
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                // Add the Response time information in the Response headers.   
                _logger.LogInformation("Response Time for "+ httpContext.Request.Path +" : " + responseTimeForCompleteRequest);
                return Task.CompletedTask;
            });
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class PageResponseMiddlewareExtensions
    {
        public static IApplicationBuilder UsePageResponseMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<PageResponseMiddleware>();
        }
    }
}
