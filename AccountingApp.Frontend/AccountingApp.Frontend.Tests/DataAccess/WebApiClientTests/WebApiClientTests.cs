using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Utils;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.WebApiClientTests
{
    public abstract class WebApiClientTests<T> where T : class, new()
    {
        protected const string TestUrl = "https://test.test/";

        protected abstract IEqualityComparer<T> Comparer { get; }

        protected virtual Mock<HttpMessageHandler> HttpMessageHandlerMock { get; }
        protected virtual HttpClient HttpClient => new HttpClient(HttpMessageHandlerMock.Object);
        protected virtual IProtectedMock<HttpMessageHandler> ProtectedHttpMessageHandlerMock
            => HttpMessageHandlerMock.Protected();

        private protected WebApiClient<T> WebApiClient => new WebApiClient<T>(HttpClient);

        public WebApiClientTests()
        {
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }

        [Fact]
        public void SetAccessToken_TheTokenIsNull_ThrowsAgrumentNullException()
        {
            var webApiClient = new WebApiClient<T>(HttpClient);
            AccessToken accessToken = null;

            Assert.Throws<ArgumentNullException>(
                () => webApiClient.SetAccessToken(accessToken)
            );
        }

        [Fact]
        public void SetAccessToken_TheTokenValueIsNull_ThrowsNullReferenceException()
        {
            var webApiClient = new WebApiClient<T>(HttpClient);
            AccessToken accessToken = new AccessToken()
            {
                Value = null,
            };

            Assert.Throws<NullReferenceException>(
                () => webApiClient.SetAccessToken(accessToken)
            );
        }

        [Fact]
        public void SetAccessToken_TheTokenIsValid_DoesNotThrow()
        {
            var webApiClient = new WebApiClient<T>(HttpClient);
            AccessToken accessToken = new AccessToken()
            {
                Value = Guid.NewGuid().ToString(),
            };

            webApiClient.SetAccessToken(accessToken);
        }

        [Fact]
        public void Get_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = WebApiClient.Get<T>(TestUrl).Result;

            Assert.Equal(AccountingApiResult.ServerUnreachable, result);
        }

        [Fact]
        public void Get_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = WebApiClient.Get<T>(TestUrl).Result;

            Assert.Equal(AccountingApiResult.Unauthorized, result);
        }

        [Fact]
        public void Get_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = WebApiClient.Get<T>(TestUrl).Result;

            Assert.Equal(AccountingApiResult.Error, result);
        }

        [Fact]
        public void Get_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            var model = new T();
            var content = JsonContent.Create(model);

            ProtectedHttpMessageHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.OK) 
                    { 
                        Content = content 
                    }
                );

            var (responseModel, result) = WebApiClient.Get<T>(TestUrl).Result;

            Assert.Equal(AccountingApiResult.Ok, result);
            Assert.Equal(model, responseModel, Comparer);
        }

        [Fact]
        public void Post_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = WebApiClient.Post<object>(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.ServerUnreachable, result);
        }

        [Fact]
        public void Post_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = WebApiClient.Post<object>(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.Unauthorized, result);
        }

        [Fact]
        public void Post_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = WebApiClient.Post<object>(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.Error, result);
        }

        [Fact]
        public void Post_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            var content = JsonContent.Create(new T());

            ProtectedHttpMessageHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.OK) 
                    { 
                        Content = content 
                    }
                );

            var (responseModel, result) = WebApiClient.Post<T>(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.Ok, result);
        }

        [Fact]
        public void Delete_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var result = WebApiClient.Delete(TestUrl, Guid.Empty).Result;

            Assert.Equal(AccountingApiResult.ServerUnreachable, result);
        }

        [Fact]
        public void Delete_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var result = WebApiClient.Delete(TestUrl, Guid.Empty).Result;

            Assert.Equal(AccountingApiResult.Unauthorized, result);
        }

        [Fact]
        public void Delete_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var result = WebApiClient.Delete(TestUrl, Guid.Empty).Result;

            Assert.Equal(AccountingApiResult.Error, result);
        }

        [Fact]
        public void Delete_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            SetupOkStatusCode();

            var result = WebApiClient.Delete(TestUrl, Guid.Empty).Result;

            Assert.Equal(AccountingApiResult.Ok, result);
        }

        [Fact]
        public void Update_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var result = WebApiClient.Update(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.ServerUnreachable, result);
        }

        [Fact]
        public void Update_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var result = WebApiClient.Update(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.Unauthorized, result);
        }

        [Fact]
        public void Update_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var result = WebApiClient.Update(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.Error, result);
        }

        [Fact]
        public void Update_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            SetupOkStatusCode();

            var result = WebApiClient.Update(TestUrl, new T()).Result;

            Assert.Equal(AccountingApiResult.Ok, result);
        }

        protected virtual void SetupUnreachableServer()
        {
            ProtectedHttpMessageHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new HttpRequestException());
        }

        protected virtual void SetupUnauthorizedUser()
        {
            ProtectedHttpMessageHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.Unauthorized)
                );
        }

        protected virtual void SetupErrorStatusCode()
        {
            ProtectedHttpMessageHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.BadRequest)
                );
        }

        protected virtual void SetupOkStatusCode()
        {
            ProtectedHttpMessageHandlerMock
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.OK)
                );
        }
    }
}
