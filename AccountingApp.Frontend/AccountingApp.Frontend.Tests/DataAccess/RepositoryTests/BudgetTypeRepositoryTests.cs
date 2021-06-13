using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public class BudgetTypeRepositoryTests : BudgetModelRepositoryTests<BudgetType>
    {
        protected override BudgetTypeRepository Repository { get; }

        public BudgetTypeRepositoryTests()
        {
            Repository = new BudgetTypeRepository(HttpClient);
        }

        [Fact]
        public void GetAll_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = Repository.GetAll().Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void GetAll_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = Repository.GetAll().Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void GetAll_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = Repository.GetAll().Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void GetAll_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
        {
            var model = new List<BudgetType>();
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

            var (_, result) = Repository.GetAll().Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }

        [Fact]
        public void GetAll_WebApiRespondenWithHttpOk_ReturnsCollectionWithExpectedNumberOfElements()
        {
            var expectedModel = new List<BudgetType> { new BudgetType(), new BudgetType() };
            var content = JsonContent.Create(expectedModel);

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

            var (model, _) = Repository.GetAll().Result;

            Assert.Equal(expectedModel.Count, model.Count());
        }
    }
}
