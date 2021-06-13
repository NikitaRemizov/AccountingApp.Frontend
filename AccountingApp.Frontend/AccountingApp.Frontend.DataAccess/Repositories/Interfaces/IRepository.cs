using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories.Interfaces
{
    public interface IRepository<T> where T : BudgetModel 
    {
        Task<(Guid, DataAccessResult)> Create(T model);
        Task<DataAccessResult> Delete(Guid id);
        Task<DataAccessResult> Update(T model);
        void SetAccessToken(AccessToken token);
    }
}
