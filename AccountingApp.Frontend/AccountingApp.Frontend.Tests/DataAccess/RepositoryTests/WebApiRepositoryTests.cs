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
