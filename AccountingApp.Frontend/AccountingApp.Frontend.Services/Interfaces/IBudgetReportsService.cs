using AccountingApp.Frontend.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services.Interfaces
{
    public interface IBudgetReportsService : IService
    {
        Task<(IEnumerable<BudgetReport>, ServiceResult)> GetReport(BudgetReportType type, BudgetReportTimeSpan timeSpan);
    }
}
