using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Models;
using System.Net.Http;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public abstract class Repository<T> where T : class
    {
        private protected WebApiClient<T> Client { get; private set; }

        protected Repository(HttpClient httpClient)
        {
            Client = new WebApiClient<T>(httpClient);
        }

        public void SetAccessToken(AccessToken token)
        {
            Client.SetAccessToken(token);
        }
    }
}
