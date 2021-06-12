using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Utils.DependencyInjection.Mapping;
using Autofac;
using System;

namespace AccountingApp.Frontend.Utils.DependencyInjection
{
    public class ServicesModule : Module
    {
        public Uri BaseAddress { get; }

        public ServicesModule(Uri baseAddress)
        {
            BaseAddress = baseAddress;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataAccessModule(BaseAddress));

            builder
                .RegisterType<AccountService>()
                .As<IAccountService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BudgetTypesService>()
                .As<IBudgetTypesService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BudgetChangesService>()
                .As<IBudgetChangesService>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<BudgetReportsService>()
                .As<IBudgetReportsService>()
                .InstancePerLifetimeScope();
        }
    }
}
