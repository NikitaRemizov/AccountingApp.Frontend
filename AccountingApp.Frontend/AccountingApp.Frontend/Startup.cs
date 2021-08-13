using AccountingApp.Frontend.Utils.DependencyInjection;
using AccountingApp.Frontend.Utils.Mapping;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using System;

namespace AccountingApp.Frontend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public MapperConfiguration MapperConfiguration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            MapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceMappingProfile>();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ProtectedBrowserStorage, ProtectedLocalStorage>();
            services.AddHttpClient();
            services.AddMudServices();
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var baseUrl = new Uri(Configuration.GetValue<string>("ApiBaseUrl"));
            builder.RegisterModule(new ServicesModule(baseUrl));

            builder
                .Register(provider => MapperConfiguration.CreateMapper())
                .As<IMapper>()
                .InstancePerDependency();

            builder
                .RegisterType<ProtectedLocalStorage>()
                .As<ProtectedBrowserStorage>()
                .InstancePerLifetimeScope();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
