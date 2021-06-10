using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public class BudgetTypes : BudgetModels<BudgetType>, IBudgetTypes
    {
        private readonly ApiEndpoint _endpoint;

        public BudgetTypes(WebApiClient<BudgetType> client, AccountingApiEndpoints apiEndpoints)
            : base(client, apiEndpoints.BudgetType.Path)
        {
            _endpoint = apiEndpoints.BudgetType;
        }

        public async Task<(IEnumerable<BudgetType>, AccountingApiResult)> GetAll()
        {
            return await Client.Get<List<BudgetType>>(_endpoint.Path);
        }
    }
}
