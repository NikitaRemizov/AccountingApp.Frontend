using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<(AccessToken, AccountingApiResult)> Register(User user);
        Task<(AccessToken, AccountingApiResult)> Login(User user);
    }
}
