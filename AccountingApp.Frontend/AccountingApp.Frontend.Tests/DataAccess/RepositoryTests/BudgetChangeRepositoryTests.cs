using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using Moq;
using Moq.Protected;
using System;
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
    public class BudgetChangeRepositoryTests : BudgetModelRepositoryTests<BudgetChange>
    {
        protected override BudgetChangeRepository Repository { get; }

        public BudgetChangeRepositoryTests()
        {
            Repository = new BudgetChangeRepository(HttpClient);
        }

        [Fact]
        public void GetForDate_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = Repository.GetForDate(DateTime.Today).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void GetForDate_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = Repository.GetForDate(DateTime.Today).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void GetForDate_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = Repository.GetForDate(DateTime.Today).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void GetForDate_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
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

            var (_, result) = Repository.GetForDate(DateTime.Today).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }

        [Fact]
        public void GetForDate_WebApiRespondenWithHttpOk_ReturnsCollectionWithExpectedNumberOfElements()
        {
            var expectedModel = new List<BudgetChange> { new BudgetChange(), new BudgetChange() };
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

            var (model, _) = Repository.GetForDate(DateTime.Today).Result;

            Assert.Equal(expectedModel.Count, model.Count());
        }

        [Fact]
        public void GetBetweenDates_HttpClientThrowsHttpRequestException_ReturnsServerUnreachable()
        {
            SetupUnreachableServer();

            var (_, result) = Repository.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(DataAccessResult.ServerUnreachable, result);
        }

        [Fact]
        public void GetBetweenDates_WebApiRespondenWithHttpUnauthorized_ReturnsApiResultUnauthorized()
        {
            SetupUnauthorizedUser();

            var (_, result) = Repository.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(DataAccessResult.Unauthorized, result);
        }

        [Fact]
        public void GetBetweenDates_WebApiRespondenWithHttpBadRequest_ReturnsApiResultError()
        {
            SetupErrorStatusCode();

            var (_, result) = Repository.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(DataAccessResult.Error, result);
        }

        [Fact]
        public void GetBetweenDates_WebApiRespondenWithHttpOk_ReturnsApiResultOk()
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

            var (_, result) = Repository.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(DataAccessResult.Ok, result);
        }

        [Fact]
        public void GetBetweenDates_WebApiRespondenWithHttpOk_ReturnsCollectionWithExpectedNumberOfElements()
        {
            var expectedModel = new List<BudgetChange> { new BudgetChange(), new BudgetChange() };
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

            var (model, _) = Repository.GetBetweenDates(DateTime.Today, DateTime.Today).Result;

            Assert.Equal(expectedModel.Count, model.Count());
        }
    }
}
