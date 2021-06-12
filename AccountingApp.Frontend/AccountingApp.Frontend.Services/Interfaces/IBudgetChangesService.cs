using AccountingApp.Frontend.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services.Interfaces
{
    public interface IBudgetChangesService : IBudgetModelsService<BudgetChange>
    {
        Task<(IEnumerable<BudgetChange>, ServiceResult)> GetForDate(DateTime date);
        Task<(IEnumerable<BudgetChange>, ServiceResult)> GetBetweenDates(DateTime from, DateTime to);
    }
}
