using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.Services
{
    public class BudgetReportsServiceTests : ServiceTests<IBudgetChangeRepository, BudgetReportsService>
    {
        private const BudgetReportType DefaultType = BudgetReportType.Income;
        private const BudgetReportTimeSpan DefaultTimeSpan = BudgetReportTimeSpan.Day;

        protected override IMock<IBudgetChangeRepository> RepositoryIMock { get; }

        protected override BudgetReportsService Service
        {
            get
            {
                if (_service is null)
                {
                    _service = new BudgetReportsService(
                        MapperConfiguration.CreateMapper(),
                        RepositoryIMock.Object
                    );
                }
                return _service;
            } 
        }

        private BudgetReportsService _service;

        public BudgetReportsServiceTests()
        {
            RepositoryIMock = new Mock<IBudgetChangeRepository>(MockBehavior.Strict);
        }

        [Fact]
        public void GetReport_RepositoryReturnsError_RepositoryGetForDateCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            Service.GetReport(DefaultType, BudgetReportTimeSpan.Day).Wait();

            RepositoryMock
                .Verify(r => r.GetForDate(It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public void GetReport_RepositoryReturnsError_RepositoryGetBetweenDatedCalledOnce()
        {
            RepositoryMock
                .Setup(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            Service.GetReport(DefaultType, BudgetReportTimeSpan.Month).Wait();

            RepositoryMock
                .Verify(r => r.GetBetweenDates(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public void GetReport_RepositoryReturnsError_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Error));

            var (_, result) = Service.GetReport(DefaultType, DefaultTimeSpan).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetReport_RepositoryReturnsServerUnreacheble_ReturnsServiceResultError()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.ServerUnreachable));

            var (_, result) = Service.GetReport(DefaultType, DefaultTimeSpan).Result;

            Assert.Equal(ServiceResult.Error, result);
        }

        [Fact]
        public void GetReport_RepositoryReturnsUnauthorized_ReturnsServiceResultUnautorized()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Unauthorized));

            var (_, result) = Service.GetReport(DefaultType, DefaultTimeSpan).Result;

            Assert.Equal(ServiceResult.Unauthorized, result);
        }

        [Fact]
        public void GetReport_RepositoryReturnsOk_ReturnsServiceResultOk()
        {
            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (new List<Shared.Models.BudgetChange>(), DataAccessResult.Ok));

            var (_, result) = Service.GetReport(DefaultType, DefaultTimeSpan).Result;

            Assert.Equal(ServiceResult.Ok, result);
        }

        [Fact]
        public void GetReport_BudgetChangesContainOnlyOneType_ReportAmountEqualToSumOfBudgetChangesAmountsForTheSameBudgetType()
        {
            var typeId = Guid.NewGuid();
            var budgetChanges = new List<Shared.Models.BudgetChange>()
            {
                new Shared.Models.BudgetChange
                {
                    BudgetTypeId = typeId,
                    Amount = 1000
                },
                new Shared.Models.BudgetChange
                {
                    BudgetTypeId = typeId,
                    Amount = 500
                }
            };

            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (budgetChanges, DataAccessResult.Ok));

            var (reports, _) = Service.GetReport(DefaultType, DefaultTimeSpan).Result;

            Assert.Equal(budgetChanges.Sum(b => b.Amount), reports.First().Amount);
        }

        [Fact]
        public void GetReport_BudgetChangesContainDifferentTypes_ReportAmountIsNotEqualToSumOfBudgetChangesAmountsForDifferentBudgetTypes()
        {
            var budgetChanges = new List<Shared.Models.BudgetChange>()
            {
                new Shared.Models.BudgetChange
                {
                    BudgetTypeId = Guid.NewGuid(),
                    Amount = 1000
                },
                new Shared.Models.BudgetChange
                {
                    BudgetTypeId = Guid.NewGuid(),
                    Amount = 500
                }
            };

            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (budgetChanges, DataAccessResult.Ok));

            var (reports, _) = Service.GetReport(DefaultType, DefaultTimeSpan).Result;

            Assert.NotEqual(budgetChanges.Sum(b => b.Amount), reports.First().Amount);
        }


        [Fact]
        public void GetReport_BudgetChangesContainsRecordsWithNegativeAmount_ReturnsPositiveValueOfExpenses()
        {
            var typeId = Guid.NewGuid();
            var budgetChanges = new List<Shared.Models.BudgetChange>()
            {
                new Shared.Models.BudgetChange
                {
                    BudgetTypeId = typeId,
                    Amount = -1000
                },
                new Shared.Models.BudgetChange
                {
                    BudgetTypeId = typeId,
                    Amount = -500
                }
            };

            RepositoryMock
                .Setup(r => r.GetForDate(It.IsAny<DateTime>()))
                .ReturnsAsync(() => (budgetChanges, DataAccessResult.Ok));

            var (reports, _) = Service.GetReport(BudgetReportType.Expense, DefaultTimeSpan).Result;

            Assert.Equal(Math.Abs(budgetChanges.Sum(b => b.Amount)), reports.First().Amount);
        }
    }
}
