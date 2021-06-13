using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<(AccessToken, DataAccessResult)> Register(User user);
        Task<(AccessToken, DataAccessResult)> Login(User user);
    }
}
