using AccountingApp.Frontend.DataAccess.Utils;
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

        protected async Task ProcessResult(AccountingApiResult result, string errorMessage)
        {
            if (result == AccountingApiResult.Unauthorized)
            {
                NavigationManager.NavigateToAuthentification();
            }
            if (result != AccountingApiResult.Ok)
            {
                await DialogService.ShowMessageBox(
                    "Error", errorMessage);
            }
        }

        protected async Task ProcessResult(AccountingApiResult result)
        {
            await ProcessResult(result, result.ToPageMessage());
        }
    }
}
