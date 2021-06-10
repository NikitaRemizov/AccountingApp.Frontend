using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using Moq;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public class AccountsTests : WebApiRepositoryTests<User>
    {
        protected override Accounts Repository { get; }

        public AccountsTests()
        {
            Repository = new Accounts(Client);
        }

        [Fact]
        public void Login_CallMethod_ClientPostCalledOnce()
        {
            SetupMockPostMethod();

            Repository.Login(new User()).Wait();

            MockVerifyPostCalledOnce();
        }

        [Fact]
        public void Register_CallMethod_ClientPostCalledOnce()
        {
            SetupMockPostMethod();

            Repository.Register(new User()).Wait();

            MockVerifyPostCalledOnce();
        }

        protected void SetupMockPostMethod()
        {
            ClientMock
                .Setup(c => c.Post<AccessToken>(It.IsAny<string>(), It.IsAny<User>()))
                .ReturnsAsync(() => (new AccessToken(), AccountingApiResult.Ok));
        }

        protected void MockVerifyPostCalledOnce()
        {
            ClientMock.Verify(
                c => c.Post<AccessToken>(It.IsAny<string>(), It.IsAny<User>()),
                Times.Once
            );
        }
    }
}
