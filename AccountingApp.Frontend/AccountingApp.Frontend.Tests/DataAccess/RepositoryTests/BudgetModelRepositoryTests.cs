using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public abstract class BudgetModelRepositoryTests<T> : RepositoryTests<T> where T : BudgetModel, new()
    {
        protected abstract override BudgetModelRepository<T> Repository { get; }

        [Fact]
        public void Create_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = Repository.Create(new T()).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void Create_HttpClientThrowsHttpRequestException_ReturnsGuidEmpty()
        {
            SetupUnreachableServer();

            var (id, _) = Repository.Create(new T()).Result;

            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public void Create_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = Repository.Create(new T()).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void Create_WebApiRespondenWithHttpUnauthorized_ReturnsGuidEmpty()
        {
            SetupUnauthorizedUser();

            var (id, _) = Repository.Create(new T()).Result;

            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public void Create_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = Repository.Create(new T()).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void Create_WebApiRespondenWithHttpBadRequest_ReturnsGuidEmpty()
        {
            SetupErrorStatusCode();

            var (id, _) = Repository.Create(new T()).Result;

            Assert.Equal(Guid.Empty, id);
        }

        [Fact]
        public void Create_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
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

            var (_, result) = Repository.Create(new T()).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }

        [Fact]
        public void Create_WebApiRespondenWithHttpOk_ReturnsValidModelId()
        {
            var modelToCreate = new T { Id = Guid.NewGuid() };
            var content = JsonContent.Create(modelToCreate);

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

            var (id, _) = Repository.Create(modelToCreate).Result;

            Assert.Equal(id, modelToCreate.Id);
        }

        [Fact]
        public void Delete_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var result = Repository.Delete(Guid.Empty).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void Delete_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var result = Repository.Delete(Guid.Empty).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void Delete_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var result = Repository.Delete(Guid.Empty).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void Delete_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            SetupOkStatusCode();

            var result = Repository.Delete(Guid.Empty).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }

        [Fact]
        public void Update_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var result = Repository.Update(new T()).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void Update_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var result = Repository.Update(new T()).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void Update_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var result = Repository.Update(new T()).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void Update_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            SetupOkStatusCode();

            var result = Repository.Update(new T()).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }
    }
}
