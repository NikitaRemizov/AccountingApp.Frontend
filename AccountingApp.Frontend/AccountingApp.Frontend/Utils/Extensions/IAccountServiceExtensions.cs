using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Shared.Models;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class IAccountServiceExtensions
    {
        public static async Task<bool> TrySetAccessToken<T>(this IAccountService account, IRepository<T> repository) where T : BudgetModel
        {
            var accessToken = await account.GetToken();
            if (accessToken is null)
            {
                return false;
            }
            repository.SetAccessToken(accessToken);
            return true;
        }
    }
}
