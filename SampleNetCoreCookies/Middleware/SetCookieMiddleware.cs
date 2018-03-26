using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Middleware
{
    public static class SetCookieMiddlewareExtensions
    {
        public static IApplicationBuilder UseSetCookieMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SetCookieMiddleware>();
        }
    }

    public class SetCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public SetCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var cookieExists = context.Request.Cookies["SampleNetCoreCookie"];
            if (string.IsNullOrEmpty(cookieExists))
            {
                // cookie does not exist, so add a unique new one
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddMinutes(1);
                var guid = $"{DateTime.Now} - {Guid.NewGuid()}";
                context.Response.Cookies.Append("SampleNetCoreCookie", guid, options);
            }
            else
            {
                // cookie exists, so fetch it and display it here
                var val = context.Request.Cookies["SampleNetCoreCookie"];
                Console.WriteLine(val);
            }

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
