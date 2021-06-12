using AccountingApp.Frontend.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services.Interfaces
{
    public interface IBudgetTypesService : IBudgetModelsService<BudgetType>
    {
        Task<(IEnumerable<BudgetType>, ServiceResult)> GetAll();
    }
}
