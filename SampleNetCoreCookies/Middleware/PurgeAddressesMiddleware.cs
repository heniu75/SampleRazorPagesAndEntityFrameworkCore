using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using SampleNetCoreCookies.Data;
using System.Threading.Tasks;

namespace SampleNetCoreCookies.Middleware
{
    public static class JanitorMiddlewareExtensions
    {
        public static IApplicationBuilder UseJanitorsMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddressJanitorMiddleware>();
        }
    }

    public class AddressJanitorMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJanitor<Address> _janitor;

        public AddressJanitorMiddleware(RequestDelegate next, IJanitor<Address> janitor)
        {
            _next = next;
            _janitor = janitor;
        }

        public Task InvokeAsync(HttpContext context)
        {
            _janitor.Run();

            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
