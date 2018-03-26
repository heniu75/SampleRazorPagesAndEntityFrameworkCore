using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleNetCoreCookies.Data;
using SampleNetCoreCookies.Filters;
using SampleNetCoreCookies.Middleware;

namespace SampleNetCoreCookies
{
    public class Startup
    {
        private readonly ILoggerFactory _loggerFactory;

        public Startup(IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            Configuration = configuration;
            _loggerFactory = loggerFactory;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddScoped<ClassConsoleLogActionOneFilter>();

            services.AddMvc(
                config =>
                {
                    config.Filters.Add(new GlobalFilter(_loggerFactory));
                    config.Filters.Add(new GlobalLoggingExceptionFilter(_loggerFactory));
                });

            // Service mappings
            services.AddTransient<IRandomGenerator, RandomGenerator>();
            services.AddTransient<IRepository<Address>, AddressRepository>();
            services.AddTransient<IJanitor<Address>, AddressJanitor>();
            services.AddSingleton<IDateProvider, DateProvider>();


            // IOptions Configuration
            services.Configure<CustomLiteDbContextOptions>(Configuration.GetSection("CustomLiteDbContextOptions"));
            services.Configure<ApplicationOptions>(Configuration.GetSection("ApplicationOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseJanitorsMiddleware();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseSetCookieMiddleware();
            app.UseCreateAddressMiddleware();
            app.UseMvc();
        }
    }
}
