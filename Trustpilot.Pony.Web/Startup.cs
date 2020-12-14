using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Trustpilot.Pony.Core.Api;
using Trustpilot.Pony.Web.Services;

namespace Trustpilot.Pony.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IMazeService, MazeService>();
            services.AddHttpClient<PonyApiClient>().ConfigureHttpClient(client => client.BaseAddress = new Uri(Configuration["TrustpilotBaseUrl"]));

            services.AddControllersWithViews().AddNewtonsoftJson();

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            if (Env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (Env.IsDevelopment())
                {
                    if (Debugger.IsAttached)
                    {
                        spa.UseReactDevelopmentServer("start");
                    }
                    else
                    {
                        spa.UseProxyToSpaDevelopmentServer("http://localhost:3000");
                    }
                }
            });
        }
    }
}
