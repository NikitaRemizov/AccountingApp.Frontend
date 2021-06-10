using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public class AccountsTests : WebApiRepositoryTests<User>
    {
        protected override Accounts Repository { get; }

        public AccountsTests()
        {
            Repository = new Accounts(Client, ApiEndpoints);
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
