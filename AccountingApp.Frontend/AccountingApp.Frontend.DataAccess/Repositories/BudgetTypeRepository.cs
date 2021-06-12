using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public class BudgetTypeRepository : BudgetModelRepository<BudgetType>, IBudgetTypeRepository
    {
        public BudgetTypeRepository(HttpClient client)
            : base(client, "budget/type")
        {
        }

        public async Task<(IEnumerable<BudgetType>, AccountingApiResult)> GetAll()
        {
            return await Client.Get<List<BudgetType>>(_endpointPath);
        }
    }
}
