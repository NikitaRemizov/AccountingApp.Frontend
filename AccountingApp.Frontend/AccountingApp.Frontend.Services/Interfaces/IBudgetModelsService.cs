using AccountingApp.Frontend.Services.Models;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services.Interfaces
{
    public interface IBudgetModelsService<T> : IBudgetService
    {
        Task<(Guid, ServiceResult)> Create(T model);
        Task<ServiceResult> Delete(Guid id);
        Task<ServiceResult> Update(T model);
    }
}
