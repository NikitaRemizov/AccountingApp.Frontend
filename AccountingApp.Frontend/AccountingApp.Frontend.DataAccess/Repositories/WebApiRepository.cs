using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Models;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public abstract class WebApiRepository<T> where T : class
    {
        private protected WebApiClient<T> Client { get; private set; }

        protected WebApiRepository(WebApiClient<T> client)
        {
            Client = client;
        }

        public void SetAccessToken(AccessToken token)
        {
            Client.SetAccessToken(token);
        }
    }
}
