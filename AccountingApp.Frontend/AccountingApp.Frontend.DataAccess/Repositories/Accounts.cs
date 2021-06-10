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
        public Accounts(WebApiClient<User> client)
            : base(client)
        {
        }

        public async Task<(AccessToken, AccountingApiResult)> Login(User user)
        {
            return await Client.Post<AccessToken>("login" , user);
        }

        public async Task<(AccessToken, AccountingApiResult)> Register(User user)
        {
            return await Client.Post<AccessToken>("register", user);
        }
    }
}
