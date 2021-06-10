using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public abstract class BudgetModels<T> : WebApiRepository<T>, IRepository<T> where T : BudgetModel
    {
        private readonly string _endpointPath;
        private protected BudgetModels(WebApiClient<T> client, string endpointPath) 
            : base(client)
        {
            _endpointPath = endpointPath;
        }

        public async Task<(Guid, AccountingApiResult)> Create(T model)
        {
            var (createdItem, result) = await Client.Post<T>(_endpointPath, model);
            return (createdItem?.Id ?? Guid.Empty, result); 
        }

        public async Task<AccountingApiResult> Delete(Guid id)
        {
            return await Client.Delete(_endpointPath, id);
        }

        public async Task<AccountingApiResult> Update(T model)
        {
            return await Client.Update(_endpointPath, model);
        }
    }
}
