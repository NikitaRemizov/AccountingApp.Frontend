using AccountingApp.Frontend.Services.Interfaces;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class IAccountServiceExtensions
    {
        public static async Task<bool> TrySetAccessToken(this IAccountService account, IService service)
        {
            var accessToken = await account.GetToken();
            if (accessToken is null)
            {
                return false;
            }
            service.SetAccessToken(accessToken);
            return true;
        }
    }
}
