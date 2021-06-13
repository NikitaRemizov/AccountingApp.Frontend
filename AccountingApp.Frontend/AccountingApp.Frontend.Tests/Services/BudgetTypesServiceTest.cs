using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Models;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AccountingApp.Frontend.Tests.Services
{
    public class BudgetTypesServiceTest : BudgetModelsServiceTests<BudgetType, Shared.Models.BudgetType>
    {
        protected override BudgetTypesService Service { 
            get
            {
                if (_service is null)
                {
                    _service = new BudgetTypesService(
                        MapperConfiguration.CreateMapper(),
                        RepositoryIMock.Object
                    );
                }
                return _service;
            } 
        }

        protected new Mock<IBudgetTypeRepository> RepositoryMock 
            => (RepositoryIMock as Mock).As<IBudgetTypeRepository>();

        protected override IMock<IBudgetTypeRepository> RepositoryIMock { get; } 

        private BudgetTypesService _service;

        public BudgetTypesServiceTest()
        {
            RepositoryIMock = new Mock<IBudgetTypeRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void GetAll_RepositoryDeleteCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => (new List<Shared.Models.BudgetType>(), DataAccessResult.Error));

            Service.GetAll().Wait();

            RepositoryMock
                .Verify(r => r.GetAll(), Times.Once);
        }

        [Fact]
        public void GetAll_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => (new List<Shared.Models.BudgetType>(), DataAccessResult.Error));

            var (_, result) = Service.GetAll().Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetAll_RepositoryReturnsServerUnreachable_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => (new List<Shared.Models.BudgetType>(), DataAccessResult.ServerUnreachable));

            var (_, result) = Service.GetAll().Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetAll_RepositoryReturnsUnauthorized_ReturnsServiceResultUnauthorized()
        {
            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => (new List<Shared.Models.BudgetType>(), DataAccessResult.Unauthorized));

            var (_, result) = Service.GetAll().Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void GetAll_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => (new List<Shared.Models.BudgetType>(), DataAccessResult.Ok));

            var (_, result) = Service.GetAll().Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void GetAll_RepositoryReturnsOk_ReturnsBudgetTypesWithExpectedSize()
        {
            var expectedBudgetTypes = new List<Shared.Models.BudgetType>() 
            { 
                new Shared.Models.BudgetType(), 
                new Shared.Models.BudgetType() 
            };

            RepositoryMock
                .Setup(r => r.GetAll())
                .ReturnsAsync(() => (expectedBudgetTypes, DataAccessResult.Ok));

            var (budgetTypes, result) = Service.GetAll().Result;

            Assert.Equal(expectedBudgetTypes.Count, budgetTypes.Count());
        }
    }
}
