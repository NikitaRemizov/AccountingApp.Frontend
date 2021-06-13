using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Utils;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Infrastructure
{
    internal class WebApiClient<TIn> where TIn : class
    {
        private HttpClient Client { get; set; }

        public WebApiClient(HttpClient client)
        {
            Client = client;
        }

        public virtual void SetAccessToken(AccessToken token)
        {
            if (token is null)
            {
                throw new ArgumentNullException(nameof(token));
            }
            if (token.Value is null)
            {
                throw new NullReferenceException(
                    $"The parameter '{nameof(token.Value)}' must not be null");
            }
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token.Value);
        }

        public virtual async Task<(T, DataAccessResult)> Get<T>(string relativeUrl) where T : class, new()
        {
            var response = await CallAndCheckConnection(
                () => Client.GetAsync(relativeUrl));
            var result = Result(response);
            if (result != DataAccessResult.Ok)
            {
                return (null, result);
            }
            return (await response.Content.ReadAsAsync<T>(), result);
        }

        public virtual async Task<(T, DataAccessResult)> Post<T>(string relativeUrl, TIn model) where T : class
        {
            var response = await CallAndCheckConnection(
                () => Client.PostAsJsonAsync(relativeUrl, model));
            var result = Result(response);
            if (result != DataAccessResult.Ok)
            {
                return (null, result);
            }
            return (await response.Content.ReadAsAsync<T>(), result);
        }

        public virtual async Task<DataAccessResult> Delete(string relativeUrl, Guid id)
        {
            var requestUrl = relativeUrl + $"/{id}";
            var response = await CallAndCheckConnection(
                () => Client.DeleteAsync(requestUrl));
            return Result(response);
        }

        public virtual async Task<DataAccessResult> Update(string relativeUrl, TIn model)
        {
            var response = await CallAndCheckConnection(
                () => Client.PutAsJsonAsync(relativeUrl, model));
            return Result(response);
        }

        protected virtual async Task<HttpResponseMessage> CallAndCheckConnection(Func<Task<HttpResponseMessage>> func)
        {
            try
            {
                return await func();
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        protected virtual DataAccessResult Result(HttpResponseMessage response)
        {
            if (response is null)
            {
                return DataAccessResult.ServerUnreachable;
            }
            var statusCode = response.StatusCode;
            if (statusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return DataAccessResult.Unauthorized;
            }
            if (!response.IsSuccessStatusCode)
            {
                return DataAccessResult.Error;
            }
            return DataAccessResult.Ok;
        }
    }
}
