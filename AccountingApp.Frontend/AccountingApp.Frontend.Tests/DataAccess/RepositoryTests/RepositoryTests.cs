using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public abstract class RepositoryTests<T> where T : class, new()
    {
        protected const string TestUrl = "https://test.test/";

        protected abstract Repository<T> Repository { get; }
        protected virtual Mock<HttpMessageHandler> HttpMessageHandlerMock { get; }
        protected virtual HttpClient HttpClient 
            => new HttpClient(HttpMessageHandlerMock.Object) { BaseAddress = new Uri(TestUrl) };
        protected virtual IProtectedMock<HttpMessageHandler> ProtectedHttpMessageHandlerMock
            => HttpMessageHandlerMock.Protected();

        public RepositoryTests()
        {
            HttpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        }

        [Fact]
        public void SetAccessToken_TheTokenIsNull_ThrowsAgrumentNullException()
        {
            AccessToken accessToken = null;

            Assert.Throws<ArgumentNullException>(
                () => Repository.SetAccessToken(accessToken)
            );
        }

        [Fact]
        public void SetAccessToken_TheTokenValueIsNull_ThrowsNullReferenceException()
        {
            AccessToken accessToken = new AccessToken()
            {
                Value = null,
            };

            Assert.Throws<NullReferenceException>(
                () => Repository.SetAccessToken(accessToken)
            );
        }

        [Fact]
        public void SetAccessToken_TheTokenIsValid_DoesNotThrow()
        {
            AccessToken accessToken = new AccessToken()
            {
                Value = Guid.NewGuid().ToString(),
            };

            Repository.SetAccessToken(accessToken);
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
