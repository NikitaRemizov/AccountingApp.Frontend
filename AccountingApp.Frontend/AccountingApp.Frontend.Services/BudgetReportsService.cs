using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services
{
    public class BudgetReportsService : IBudgetReportsService
    {
        private const string Untyped = "UNCATEGORIZED";

        public IMapper Mapper { get; }
        protected IBudgetChangeRepository Repository { get; }

        private static readonly IEnumerable<BudgetReport> _emptyBudgetReports = new List<BudgetReport>(); 

        public BudgetReportsService(IMapper mapper, IBudgetChangeRepository repository)
        {
            Mapper = mapper;
            Repository = repository;
        }

        public async Task<(IEnumerable<BudgetReport>, ServiceResult)> GetReport(BudgetReportType type, BudgetReportTimeSpan timeSpan)
        {
            var (budgetChanges, result) = await GetBudgetChanges(timeSpan);

            if (result != AccountingApiResult.Ok)
            {
                return (_emptyBudgetReports,
                    Mapper.Map<ServiceResult>(result));
            }

            var reportRecords = budgetChanges
                .GroupBy(b => b.BudgetTypeId)
                .Select(group => new BudgetReport
                {
                    BudgetTypeName = group.First().BudgetTypeName ?? Untyped,
                    Amount = group.Sum(g => g.Amount)
                });

            return type switch
            {
                BudgetReportType.Income => (FilterIncomeRecords(reportRecords), ServiceResult.Ok),
                BudgetReportType.Expense => (FilterExpenseRecords(reportRecords), ServiceResult.Ok),
                _ => throw new ArgumentOutOfRangeException($"The invalid {nameof(BudgetReportType)} value {type}")
            };
        }

        public void SetAccessToken(string token)
        {
            Repository.SetAccessToken(
                new AccessToken { Value = token }
            );
        }

        private IEnumerable<BudgetReport> FilterIncomeRecords(IEnumerable<BudgetReport> budgetReports)
        {
            return budgetReports.Where(r => r.Amount > 0);
        }

        private IEnumerable<BudgetReport> FilterExpenseRecords(IEnumerable<BudgetReport> budgetReports)
        {
            return budgetReports
                .Where(r => r.Amount < 0)
                .Select(r =>
                {
                    r.Amount *= -1;
                    return r;
                });
        }

        private async Task<(IEnumerable<Shared.Models.BudgetChange>, AccountingApiResult)> GetBudgetChanges(BudgetReportTimeSpan timeSpan)
        {
            return timeSpan switch
            {
                BudgetReportTimeSpan.Day =>
                    await Repository.GetForDate(DateTime.Today),
                BudgetReportTimeSpan.Month =>
                    await Repository.GetBetweenDates(DateTime.Today.AddMonths(-1), DateTime.Today),
                _ => throw new ArgumentOutOfRangeException($"The invalid {nameof(BudgetReportTimeSpan)} value {timeSpan}")
            };
        }
    }
}
