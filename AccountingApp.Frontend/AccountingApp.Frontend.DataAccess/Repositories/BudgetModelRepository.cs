using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public abstract class BudgetModelRepository<T> : Repository<T>, IRepository<T> where T : BudgetModel
    {
        protected readonly string _endpointPath;

        private protected BudgetModelRepository(HttpClient client, string endpointPath) 
            : base(client)
        {
            _endpointPath = endpointPath;
        }

        public async Task<(Guid, DataAccessResult)> Create(T model)
        {
            var (createdItem, result) = await Client.Post<T>(_endpointPath, model);
            return (createdItem?.Id ?? Guid.Empty, result); 
        }

        public async Task<DataAccessResult> Delete(Guid id)
        {
            return await Client.Delete(_endpointPath, id);
        }

        public async Task<DataAccessResult> Update(T model)
        {
            return await Client.Update(_endpointPath, model);
        }
    }
}
