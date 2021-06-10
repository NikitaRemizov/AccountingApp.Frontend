using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories.Interfaces
{
    public interface IBudgetChanges : IRepository<BudgetChange>
    {
        Task<(IEnumerable<BudgetChange>, AccountingApiResult)> GetForDate(DateTime date);
        Task<(IEnumerable<BudgetChange>, AccountingApiResult)> GetBetweenDates(DateTime from, DateTime to);
    }
}
