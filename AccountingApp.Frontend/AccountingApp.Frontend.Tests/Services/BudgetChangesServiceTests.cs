using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace AccountingApp.Frontend.Tests.Services
{
    public class BudgetChangesServiceTest : BudgetModelsServiceTests<BudgetChange, Shared.Models.BudgetChange>
    {
        protected override BudgetChangesService Service { 
            get
            {
                if (_service is null)
                {
                    _service = new BudgetChangesService(
                        MapperConfiguration.CreateMapper(),
                        RepositoryIMock.Object
                    );
                }
                return _service;
            } 
        }

        protected new Mock<IBudgetChangeRepository> RepositoryMock 
            => (RepositoryIMock as Mock).As<IBudgetChangeRepository>();

        protected override IMock<IBudgetChangeRepository> RepositoryIMock { get; } 

        private BudgetChangesService _service;

        public BudgetChangesServiceTest()
        {
            RepositoryIMock = new Mock<IBudgetChangeRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void GetForDate_RepositoryDeleteCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            Service.GetForDate(DateTime.Today).Wait();

            RepositoryMock
                .Verify(r => r.GetForDate(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public void GetForDate_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            var (_, result) = Service.GetForDate(DateTime.Today).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetForDate_RepositoryReturnsServerUnreachable_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.ServerUnreachable));

            var (_, result) = Service.GetForDate(DateTime.Today).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetForDate_RepositoryReturnsUnauthorized_ReturnsServiceResultUnauthorized()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Unauthorized));

            var (_, result) = Service.GetForDate(DateTime.Today).Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void GetForDate_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Ok));

            var (_, result) = Service.GetForDate(DateTime.Today).Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void GetForDate_RepositoryReturnsOk_ReturnsBudgetTypesWithExpectedSize()
        {
            var expectedBudgetChanges = new List<Shared.Models.BudgetChange>() 
            { 
                new Shared.Models.BudgetChange(), 
                new Shared.Models.BudgetChange() 
            };

            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (expectedBudgetChanges, DataAccessResult.Ok));

            var (budgetChanges, result) = Service.GetForDate(DateTime.Today).Result;

            Assert.Equal(expectedBudgetChanges.Count, budgetChanges.Count());
        }

        [Fact]
        public void GetBetweenDates_RepositoryDeleteCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            Service.GetBetweenDates(DateTime.Today, DateTime.Today).Wait();

            RepositoryMock
                .Verify(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public void GetBetweenDates_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            var (_, result) = Service.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetBetweenDates_RepositoryReturnsServerUnreachable_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.ServerUnreachable));

            var (_, result) = Service.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetBetweenDates_RepositoryReturnsUnauthorized_ReturnsServiceResultUnauthorized()
        {
            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Unauthorized));

            var (_, result) = Service.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void GetBetweenDates_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Ok));

            var (_, result) = Service.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void GetBetweenDates_RepositoryReturnsOk_ReturnsBudgetTypesWithExpectedSize()
        {
            var expectedBudgetChanges = new List<Shared.Models.BudgetChange>() 
            { 
                new Shared.Models.BudgetChange(), 
                new Shared.Models.BudgetChange() 
            };

            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (expectedBudgetChanges, DataAccessResult.Ok));

            var (budgetChanges, result) = Service.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(expectedBudgetChanges.Count, budgetChanges.Count());
        }
    }
}
