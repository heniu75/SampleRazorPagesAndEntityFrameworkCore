using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SampleNetCoreCookies.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Middleware
{
    public static class CreateAddressMiddlewareExtensions
    {
        public static IApplicationBuilder UseCreateAddressMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CreateAddressMiddleware>();
        }
    }

    public class CreateAddressMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IRepository<Address> _addressRepo;

        public CreateAddressMiddleware(RequestDelegate next, IRepository<Address> addressRepo)
        {
            _next = next;
            _addressRepo = addressRepo;
        }

        public Task InvokeAsync(HttpContext context)
        {
            var cookieExists = context.Request.Cookies["SampleNetCoreCookie"];
            string mail;
            if (string.IsNullOrEmpty(cookieExists))
            {
                // cookie does not exist, so add a unique new one
                mail = CreateNewCookie(context);
            }
            else
            {
                // cookie exists, so fetch it and display it here
                var cookie = context.Request.Cookies["SampleNetCoreCookie"];
                var address = _addressRepo.Get(cookie);
                if (address == null)
                {
                    mail = CreateNewCookie(context);
                }
                else if (address.ExpireAt < DateTime.Now)
                {
                    _addressRepo.Delete(address.Cookie);
                    mail = CreateNewCookie(context);
                }
                else
                {
                    mail = address.EmailAddress;
                }
            }

            context.Request.Headers.Add("TempEmailAddress", mail);

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }

        private string CreateNewCookie(HttpContext context)
        {
            CookieOptions options = new CookieOptions();
            var newAddress = _addressRepo.Create();
            options.Expires = DateTime.Now.AddYears(10);
            context.Response.Cookies.Append("SampleNetCoreCookie", newAddress.Cookie, options);
            return newAddress.EmailAddress;
        }
    }
}
