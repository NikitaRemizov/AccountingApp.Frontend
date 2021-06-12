using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories.Interfaces
{
    public interface IBudgetTypeRepository : IRepository<BudgetType>
    {
        Task<(IEnumerable<BudgetType>, AccountingApiResult)> GetAll();
    }
}
