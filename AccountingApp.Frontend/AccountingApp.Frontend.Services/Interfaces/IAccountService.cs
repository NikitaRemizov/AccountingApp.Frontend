using AccountingApp.Frontend.Services.Models;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services.Interfaces
{
    public interface IAccountService
    {
        public event Action OnIsAuthentificatedChanged;
        public bool IsAuthentificated { get; }
        public Task<string> GetToken();
        public Task InitializeToken();
        public Task Login(User user);
        public Task Register(User user);
        public Task Logout();
    }
}
