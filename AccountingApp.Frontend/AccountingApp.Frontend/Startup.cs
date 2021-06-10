using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Utils.Mapping;
using AccountingApp.Shared.Models;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAccounts, Accounts>();
            services.AddScoped<IBudgetTypes, BudgetTypes>();
            services.AddScoped<IBudgetChanges, BudgetChanges>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ProtectedBrowserStorage, ProtectedLocalStorage>();
            services.AddScoped<WebApiClient<User>>();
            services.AddScoped<WebApiClient<BudgetType>>();
            services.AddScoped<WebApiClient<BudgetChange>>();

            var apiDescription = Configuration.GetSection("ApiDescription");

            var baseUrl = new Uri(apiDescription.GetValue<string>("ApiBaseUrl"));
            services.AddHttpClient<WebApiClient<User>>(
                client => client.BaseAddress = baseUrl);
            services.AddHttpClient<WebApiClient<BudgetType>>(
                client => client.BaseAddress = baseUrl);
            services.AddHttpClient<WebApiClient<BudgetChange>>(
                client => client.BaseAddress = baseUrl);

            var apiEndpoints = apiDescription.GetSection("Endpoints").Get<AccountingApiEndpoints>();
            services.AddSingleton(apiEndpoints);

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<BudgetModelsMappingProfile>();
            });
            services.AddTransient(provider => mapperConfiguration.CreateMapper());

            services.AddMudServices();
            services.AddRazorPages();
            services.AddServerSideBlazor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
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

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
