using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services;
using AccountingApp.Frontend.Services.Models;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.JSInterop;
using Moq;
using System;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace AccountingApp.Frontend.Tests.Services
{
    public class AccountServiceTests : ServiceTests<IAccountRepository, AccountService>
    {
        protected override AccountService Service { 
            get
            {
                if (_service is null)
                {
                    _service = new AccountService(
                        MapperConfiguration.CreateMapper(),
                        RepositoryIMock.Object,
                        BrowserStorage
                    );
                }
                return _service;
            } 
        }

        protected override Mock<IAccountRepository> RepositoryIMock { get; }
        protected Mock<IJSRuntime> JSRuntimeMock { get; private set; }
        protected ProtectedBrowserStorage BrowserStorage { get; private set; }
        protected IJSRuntime JSRuntime { get; private set; }

        private static AccessToken AccessToken { get; } = new AccessToken { Value = Guid.NewGuid().ToString() };
        private string StoredAccessToken { get; set; }

        private AccountService _service;

        public AccountServiceTests()
        {
            RepositoryIMock = new Mock<IAccountRepository>(MockBehavior.Strict);
            InitializeBrowserStorage();
        }

        [Fact]
        public void InitializeToken_BrowserStorageResultUnsuccessful_IJSRuntimeInvokeAsyncCalledOnce()
        {
            SetBrowserStorageTokenToNull();

            Service.InitializeToken().Wait();

            JSRuntimeMock
                .Verify(jsr => jsr.InvokeAsync<string>(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public void InitializeToken_BrowserStorageResultUnsuccessful_IsAuthentificatedFalse()
        {
            SetBrowserStorageTokenToNull();

            Service.InitializeToken().Wait();

            Assert.False(Service.IsAuthentificated);
        }

        [Fact]
        public void InitializeToken_BrowserStorageResultSuccessful_IsAuthentificatedTrue()
        {
            SetBrowserStorageTokenToAcessToken();

            Service.InitializeToken().Wait();

            Assert.True(Service.IsAuthentificated);
        }

        [Fact]
        public void GetToken_BrowserStorageResultUnsuccessful_IJSRuntimeInvokeAsyncCalledOnce()
        {
            SetBrowserStorageTokenToNull();

            Service.GetToken().Wait();

            JSRuntimeMock
                .Verify(jsr => jsr.InvokeAsync<string>(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public void GetToken_BrowserStorageResultUnsuccessful_IsAuthentificatedFalse()
        {
            SetBrowserStorageTokenToNull();

            Service.GetToken().Wait();

            Assert.False(Service.IsAuthentificated);
        }

        [Fact]
        public void GetToken_BrowserStorageResultSuccessful_IsAuthentificatedTrue()
        {
            SetBrowserStorageTokenToAcessToken();

            Service.GetToken().Wait();

            Assert.True(Service.IsAuthentificated);
        }

        [Fact]
        public void GetToken_BrowserStorageResultSuccessful_ReturnsValidAccessToken()
        {
            SetBrowserStorageTokenToAcessToken();

            var token = Service.GetToken().Result;

            Assert.Equal(AccessToken.Value, token);
        }

        [Fact]
        public void Logout_BrowserStorageContainsAccessToken_IsAuthentificatedFalse()
        {
            SetBrowserStorageTokenToAcessToken();

            Service.Logout().Wait();

            Assert.False(Service.IsAuthentificated);
        }

        [Fact]
        public void Logout_BrowserStorageContainsAccessToken_GetTokenReturnsNull()
        {
            SetBrowserStorageTokenToAcessToken();

            Service.Logout().Wait();
            var token = Service.GetToken().Result;

            Assert.Null(token);
        }

        [Fact]
        public void Login_AccountingApiResultIsError_IsAuthentificatedFalse()
        {
            SetupRepositoryLoginError();

            Service.Login(new User()).Wait();

            Assert.False(Service.IsAuthentificated);
        }

        [Fact]
        public void Login_AccountingApiResultIsError_GetAccessTokenReturnsNull()
        {
            SetupRepositoryLoginError();

            Service.Login(new User()).Wait();

            var token = Service.GetToken().Result;

            Assert.Null(token);
        }

        [Fact]
        public void Login_AccountingApiResultIsError_IJSRuntimeInvokeAsyncCalledOnce()
        {
            SetupRepositoryLoginError();

            Service.Login(new User()).Wait();

            JSRuntimeMock
                .Verify(jsr => jsr.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public void Login_AccountingApiResultIsOk_IsAuthentificatedTrue()
        {
            SetupRepositoryLoginOk();

            Service.Login(new User()).Wait();

            Assert.True(Service.IsAuthentificated);
        }

        [Fact]
        public void Login_AccountingApiResultIsOk_GetAccessTokenReturnsAccessToken()
        {
            SetupRepositoryLoginOk();

            Service.Login(new User()).Wait();

            var token = Service.GetToken().Result;

            Assert.Equal(AccessToken.Value, token);
        }

        [Fact]
        public void Login_AccountingApiResultIsOk_IJSRuntimeInvokeAsyncCalledOnce()
        {
            SetupRepositoryLoginOk();

            Service.Login(new User()).Wait();

            JSRuntimeMock
                .Verify(jsr => jsr.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<object[]>()), Times.Once);
        }

        [Fact]
        public void Register_AccountingApiResultIsError_IsAuthentificatedFalse()
        {
            SetupRepositoryRegisterError();

            Service.Register(new User()).Wait();

            Assert.False(Service.IsAuthentificated);
        }

        [Fact]
        public void Register_AccountingApiResultIsError_GetAccessTokenReturnsNull()
        {
            SetupRepositoryRegisterError();

            Service.Register(new User()).Wait();

            var token = Service.GetToken().Result;

            Assert.Null(token);
        }

        [Fact]
        public void Register_AccountingApiResultIsOk_IsAuthentificatedTrue()
        {
            SetupRepositoryRegisterOk();

            Service.Register(new User()).Wait();

            Assert.True(Service.IsAuthentificated);
        }

        [Fact]
        public void Register_AccountingApiResultIsOk_GetAccessTokenReturnsAccessToken()
        {
            SetupRepositoryRegisterOk();

            Service.Register(new User()).Wait();

            var token = Service.GetToken().Result;

            Assert.Equal(AccessToken.Value, token);
        }

        [Fact]
        public void OnIsAuthentificatedChanged_OperationChangesIsAuthentificated_EventFired()
        {
            SetupRepositoryRegisterOk();
            bool IsEventFired = false;

            Service.OnIsAuthentificatedChanged += () =>
            {
                IsEventFired = true;
            };

            Service.InitializeToken().Wait();

            Assert.True(IsEventFired);
        }

        protected void SetBrowserStorageTokenToNull()
        {
            StoredAccessToken = null;
        }

        protected void SetBrowserStorageTokenToAcessToken()
        {
            BrowserStorage.SetAsync(nameof(AccessToken), AccessToken).AsTask().Wait();
        }

        protected void SetupRepositoryLoginOk()
        {
            RepositoryMock
                .Setup(r => r.Login(It.IsAny<Shared.Models.User>()))
                .Returns(() => Task.FromResult((AccessToken, DataAccessResult.Ok)));
        }

        protected void SetupRepositoryLoginError()
        {
            RepositoryMock
                .Setup(r => r.Login(It.IsAny<Shared.Models.User>()))
                .Returns(() => Task.FromResult<(AccessToken, DataAccessResult)>((null, DataAccessResult.Error)));
        }

        protected void SetupRepositoryRegisterOk()
        {
            RepositoryMock
                .Setup(r => r.Register(It.IsAny<Shared.Models.User>()))
                .Returns(() => Task.FromResult((AccessToken, DataAccessResult.Ok)));
        }

        protected void SetupRepositoryRegisterError()
        {
            RepositoryMock
                .Setup(r => r.Register(It.IsAny<Shared.Models.User>()))
                .Returns(() => Task.FromResult<(AccessToken, DataAccessResult)>((null, DataAccessResult.Error)));
        }

        private void InitializeBrowserStorage()
        {
            var dataProtectorMock = new Mock<IDataProtector>(MockBehavior.Strict);
            dataProtectorMock
                .Setup(dp => dp.Protect(It.IsAny<byte[]>()))
                .Returns((byte[] bytes) => bytes);
            dataProtectorMock
                .Setup(dp => dp.Unprotect(It.IsAny<byte[]>()))
                .Returns((byte[] bytes) => bytes);

            var protectionProviderMock = new Mock<IDataProtectionProvider>(MockBehavior.Strict);
            protectionProviderMock
                .Setup(pp => pp.CreateProtector(It.IsAny<string>()))
                .Returns(() => dataProtectorMock.Object);

            JSRuntimeMock = new Mock<IJSRuntime>(MockBehavior.Strict);
            JSRuntimeMock
                .Setup(jsr => jsr.InvokeAsync<object>(It.IsAny<string>(), It.IsAny<object[]>()))
                .Returns(
                    (string identifier, object[] args) => ValueTask.FromResult(PerformOperation(identifier, args))
                );
            JSRuntimeMock
                .Setup(jsr => jsr.InvokeAsync<string>(It.IsAny<string>(), It.IsAny<object[]>()))
                .Returns((string identifier, object[] args) => ValueTask.FromResult(PerformOperation(identifier)));

            JSRuntime = JSRuntimeMock.Object;

            BrowserStorage = new ProtectedLocalStorage(JSRuntime, protectionProviderMock.Object);
        }

        private object PerformOperation(string identifier, object[] args)
        {
            if (identifier.Contains(".setItem"))
            {
                StoredAccessToken = (string) args[1];
            }
            else if (identifier.Contains(".removeItem"))
            {
                StoredAccessToken = null;
            }
            return new object();
        }
        private string PerformOperation(string identifier)
        {
            return StoredAccessToken;
        }
    }
}
