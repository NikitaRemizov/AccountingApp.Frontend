using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        public AccountRepository(HttpClient client)
            : base(client)
        {
        }

        public async Task<(AccessToken, DataAccessResult)> Login(User user)
        {
            return await Client.Post<AccessToken>("login" , user);
        }

        public async Task<(AccessToken, DataAccessResult)> Register(User user)
        {
            return await Client.Post<AccessToken>("register", user);
        }
    }
}
