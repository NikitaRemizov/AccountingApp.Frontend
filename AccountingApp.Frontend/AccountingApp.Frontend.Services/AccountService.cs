using AccountingApp.Frontend.DataAccess.Models;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services
{
    public class AccountService : IAccountService
    {
        private const string AccessTokenName = nameof(AccessToken);

        public event Action OnIsAuthentificatedChanged;

        public bool IsAuthentificated
        {
            get { return _isAuthentificated; }
            private set 
            {
                _isAuthentificated = value; 
                OnIsAuthentificatedChanged?.Invoke();
            }
        }


        private readonly IMapper mapper;
        private bool _isAuthentificated;
        private readonly IAccountRepository _accounts;
        private readonly ProtectedBrowserStorage _browserStorage;
        private AccessToken _accessToken; 

        public AccountService(IMapper mapper, IAccountRepository accounts, ProtectedBrowserStorage browserStorage)
        {
            this.mapper = mapper;
            _accounts = accounts;
            _browserStorage = browserStorage;
        }

        public async Task<string> GetToken()
        {
            if (_accessToken is null)
            {
                await InitializeToken();
            }
            return _accessToken.Value;
        }

        public async Task InitializeToken()
        {
            var storageResult = await _browserStorage.GetAsync<AccessToken>(AccessTokenName);
            if (!storageResult.Success)
            {
                IsAuthentificated = false;
                return;
            }
            _accessToken = storageResult.Value;
            IsAuthentificated = true;
        }

        public async Task Login(User user)
        {
            var userToLogin = mapper.Map<Shared.Models.User>(user);
            var (token, result) = await _accounts.Login(userToLogin);
            if (result != AccountingApiResult.Ok)
            {
                await Logout();
                return;
            }
            await _browserStorage.SetAsync(AccessTokenName, token);
            IsAuthentificated = true;
        }

        public async Task Register(User user)
        {
            var userToLogin = mapper.Map<Shared.Models.User>(user);
            var (token, result) = await _accounts.Register(userToLogin);
            if (result != AccountingApiResult.Ok)
            {
                await Logout();
                return;
            }
            await _browserStorage.SetAsync(AccessTokenName, token);
            IsAuthentificated = true;
        }

        public async Task Logout()
        {
            _accessToken = null;
            await _browserStorage.DeleteAsync(AccessTokenName);
            IsAuthentificated = false;
        }
    }
}
