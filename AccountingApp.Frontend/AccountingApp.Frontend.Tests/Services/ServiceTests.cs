using AccountingApp.Frontend.Utils.Mapping;
using AutoMapper;
using Moq;

namespace AccountingApp.Frontend.Tests.Services
{
    public abstract class ServiceTests<TRepository, TService> where TRepository : class
    {
        protected static MapperConfiguration MapperConfiguration { get; } =
            new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ServiceMappingProfile>();
            });

        protected abstract IMock<TRepository> RepositoryIMock { get; }
        protected Mock<TRepository> RepositoryMock => 
            (RepositoryIMock as Mock).As<TRepository>();

        protected abstract TService Service { get; }

    }
}
