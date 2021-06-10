using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public class Accounts : WebApiRepository<User>, IAccounts
    {
        private readonly AccountingApiEndpoints _apiEndpoints;

        public Accounts(WebApiClient<User> client, AccountingApiEndpoints apiEndpoints)
            : base(client)
        {
            _apiEndpoints = apiEndpoints;
        }

        public async Task<(AccessToken, AccountingApiResult)> Login(User user)
        {
            return await Client.Post<AccessToken>(_apiEndpoints.Login.Path, user);
        }

        public async Task<(AccessToken, AccountingApiResult)> Register(User user)
        {
            return await Client.Post<AccessToken>(_apiEndpoints.Register.Path, user);
        }
    }
}
