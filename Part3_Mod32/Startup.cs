using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Part3_Mod32.Middlewares;

namespace Part3_Mod32
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        private static IWebHostEnvironment statEnv;
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            statEnv = env;
            Console.WriteLine($"Launching project from: ContentRootPath: {env.ContentRootPath} and WebRootPath {env.WebRootPath}");

            if (env.IsDevelopment() || env.IsStaging())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();

            Console.WriteLine($"Launching project from: ContentRootPath: {env.ContentRootPath} and WebRootPath {env.WebRootPath}");

            app.UseMiddleware<LoggingMiddleware>(env);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync($"Welcome to the {env.ApplicationName}!");
                });
            });


            app.Map("/about", About);
            app.Map("/config", Config);

            app.UseStatusCodePages();
            //app.Run(async (context) =>
            //{
            //    int zero = 0;
            //    int result = 4 / zero;
            //    await context.Response.WriteAsync($"Page not found");
            //});

        }

        private static void About(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"{statEnv.ApplicationName} - ASP.Net Core tutorial project");
            });
        }

        private static void Config(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync($"App name: {statEnv.ApplicationName}. App running configuration: {statEnv.EnvironmentName}");
            });
        }
    }

}
