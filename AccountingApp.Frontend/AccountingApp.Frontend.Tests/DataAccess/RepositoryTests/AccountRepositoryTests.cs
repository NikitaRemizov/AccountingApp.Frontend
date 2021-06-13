using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public class AccountRepositoryTests : RepositoryTests<User>
    {
        protected override AccountRepository Repository { get; }


        public AccountRepositoryTests()
        {
            Repository = new AccountRepository(HttpClient);
        }

        [Fact]
        public void Login_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = Repository.Login(new User()).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void Login_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = Repository.Login(new User()).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void Login_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = Repository.Login(new User()).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void Login_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            var content = JsonContent.Create(new AccessToken());

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

            var (_, result) = Repository.Login(new User()).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }

        [Fact]
        public void Register_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = Repository.Register(new User()).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void Register_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = Repository.Register(new User()).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void Register_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = Repository.Register(new User()).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void Register_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            var content = JsonContent.Create(new AccessToken());

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

            var (_, result) = Repository.Register(new User()).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }
    }
}
