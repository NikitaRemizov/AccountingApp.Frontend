using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AccountingApp.Frontend.Utils.Extensions;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared
{
    public class DataAccessComponent : ComponentBase
    {
        [Inject]
        protected NavigationManager NavigationManager { get; set; }

        [Inject]
        protected IDialogService DialogService { get; set; }

        [Inject]
        protected IAccountService Account { get; set; }

        protected async Task ProcessResult(ServiceResult result, string errorMessage)
        {
            if (result == ServiceResult.Unauthorized)
            {
                NavigationManager.NavigateToAuthentification();
                await Account.Logout();
            }
            if (result != ServiceResult.Ok)
            {
                await DialogService.ShowMessageBox(
                    "Error", errorMessage);
            }
        }

        protected async Task ProcessResult(ServiceResult result)
        {
            await ProcessResult(result, result.ToPageMessage());
        }
    }
}
