using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SampleRazorPages0.Data;

namespace SampleRazorPages0
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("name");
            });
            services.AddMvc()
                .AddRazorPagesOptions(options =>
               {
                   // these are page route relative...
                   // ... so the following line will allow a user ...
                   // ... when navigating to /AllCustomers, this will route to /Customers/Index
                   options.Conventions.AddPageRoute("/Customers/Index", "AllCustomers");
               });
            

            // Map configuration to type (later, via DI, a class/model can use
            // ... the IOptions<MyCustomConfig> ctor parameter to access this!
            services.Configure<MyCustomConfig>(Configuration.GetSection("MyCustomConfigSection"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // if you uncomment this, then the app will just run and always return "Hello, World"
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello, World!");
            //});

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();

            // inline middleware
            // if the following block is un-commented
            // then the app will always just say "Hello, .NET Conf!"
            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("MVC could not match anything");
                //return;
            });
        }
    }
}
