using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class IDialogServiceExtensions
    {
        public static async Task Show<T>(this IDialogService dialogService, 
                                         Func<Task> afterSubmitAction, 
                                         IBudgetTypes budgetTypes = null, 
                                         IBudgetChanges budgetChanges = null,
                                         object viewModel = null) where T : ComponentBase
        {
            if (dialogService is null)
            {
                return;
            }

            var parameters = new DialogParameters();


            switch ((budgetChanges, budgetTypes))
            {
                case (null, not null):
                    parameters.Add("Repository", budgetTypes);
                    break;
                case (not null, not null):
                    parameters.Add("Repository", budgetChanges);
                    parameters.Add("BudgetTypeRepository", budgetTypes);
                    break;
                default:
                    break;
            }

            if (viewModel is not null)
            {
                parameters.Add("ViewModel", viewModel);
            }

            var dialogResult = await dialogService.Show<T>(null, parameters).Result;
            if (dialogResult.Cancelled)
            {
                return;
            }

            await afterSubmitAction?.Invoke();
        }
    }
}
