using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using Autofac;
using System;

namespace AccountingApp.Frontend.Services.Utils.DependencyInjection.Mapping
{
    public class DataAccessModule : Module
    {
        public Uri BaseAddress { get; }

        public DataAccessModule(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<AccountRepository>()
                .As<IAccountRepository>()
                .InstancePerLifetimeScope()
                .ConfigurePipeline(p =>
                {
                    p.Use(new HttpClientMiddleware<AccountRepository>(BaseAddress));
                });

            builder
                .RegisterType<BudgetTypeRepository>()
                .As<IBudgetTypeRepository>()
                .InstancePerLifetimeScope()
                .ConfigurePipeline(p =>
                {
                    p.Use(new HttpClientMiddleware<BudgetTypeRepository>(BaseAddress));
                });

            builder
                .RegisterType<BudgetChangeRepository>()
                .As<IBudgetChangeRepository>()
                .InstancePerLifetimeScope()
                .ConfigurePipeline(p =>
                {
                    p.Use(new HttpClientMiddleware<BudgetChangeRepository>(BaseAddress));
                });

        }
    }
}
