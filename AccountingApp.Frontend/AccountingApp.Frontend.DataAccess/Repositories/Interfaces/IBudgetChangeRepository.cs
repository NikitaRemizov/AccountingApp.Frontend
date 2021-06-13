using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories.Interfaces
{
    public interface IBudgetChangeRepository : IRepository<BudgetChange>
    {
        Task<(IEnumerable<BudgetChange>, DataAccessResult)> GetForDate(DateTime date);
        Task<(IEnumerable<BudgetChange>, DataAccessResult)> GetBetweenDates(DateTime from, DateTime to);
    }
}
