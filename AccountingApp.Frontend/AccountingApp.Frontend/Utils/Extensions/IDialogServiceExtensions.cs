using AccountingApp.Frontend.Services.Interfaces;
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
                                         IBudgetTypesService budgetTypes = null, 
                                         IBudgetChangesService budgetChanges = null,
                                         object model = null) where T : ComponentBase
        {
            if (dialogService is null)
            {
                return;
            }

            var parameters = new DialogParameters();


            switch ((budgetChanges, budgetTypes))
            {
                case (null, not null):
                    parameters.Add("Service", budgetTypes);
                    break;
                case (not null, not null):
                    parameters.Add("Service", budgetChanges);
                    parameters.Add("BudgetTypesService", budgetTypes);
                    break;
                default:
                    break;
            }

            if (model is not null)
            {
                parameters.Add("Model", model);
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
