using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories;
using Moq;
using System.Net.Http;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public abstract class WebApiRepositoryTests<T> where T : class
    {
        protected abstract WebApiRepository<T> Repository { get; }
        protected Mock<WebApiClient<T>> ClientMock { get; }
        protected WebApiClient<T> Client => ClientMock.Object;

        protected static AccountingApiEndpoints ApiEndpoints { get; } =
            new AccountingApiEndpoints
            {
                Login = new ApiEndpoint { Path = string.Empty },
                Register = new ApiEndpoint { Path = string.Empty },
                BudgetType = new ApiEndpoint { Path = string.Empty },
                BudgetChange = new BudgetChangeEndpoint
                {
                    Path = string.Empty,
                    PathForDate = string.Empty,
                    PathBetweenDates = string.Empty,
                    Arguments = new BudgetChangeApiArguments
                    {
                        FromDate = string.Empty,
                        ToDate = string.Empty,
                        SingleDate = string.Empty,
                    },
                },
            };

        public WebApiRepositoryTests()
        {
            ClientMock = new Mock<WebApiClient<T>>(MockBehavior.Strict, new HttpClient() 
            { 
                BaseAddress = new System.Uri("https://test.test"),
            });
        }

        [Fact]
        public void SetAccessToken_CallMethod_ClientSetAccessTokenCalledOnce()
        {
            ClientMock
                .Setup(c => c.SetAccessToken(It.IsAny<AccessToken>()));

            Repository.SetAccessToken(new AccessToken());

            ClientMock
                .Verify(c => c.SetAccessToken(It.IsAny<AccessToken>()), Times.Once);
        }
    }
}
