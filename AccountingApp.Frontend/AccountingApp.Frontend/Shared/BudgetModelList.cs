using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AccountingApp.Frontend.Utils.Extensions;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared
{
    public abstract class BudgetModelList<TModel> : DataAccessComponent where TModel : BudgetModel 
    {
        protected List<TModel> ModelItems { get; set; } = new List<TModel>();

        protected abstract IBudgetModelsService<TModel> Service { get; }

        [Inject]
        protected IMapper Mapper { get; set; }

        protected abstract Task InitializeItemsList();

        protected override async Task OnInitializedAsync()
        {
            if (!(await Account.TrySetAccessToken(Service)))
            {
                NavigationManager.NavigateToAuthentification();
                return;
            }

            await InitializeItemsList();
        }

        protected async Task Delete(TModel viewModel)
        {
            var id = viewModel?.Id;
            if (id is null || id == Guid.Empty)
            {
                return;
            }

            var result = await Service.Delete((Guid)id);
            await ProcessResult(result);
            if (result != ServiceResult.Ok)
            {
                return;
            }

            ModelItems.Remove(viewModel);
            StateHasChanged();
        }

        protected async Task Update(TModel viewModel)
        {
            var id = viewModel?.Id;
            if (id is null || id == Guid.Empty)
            {
                return;
            }
            var modelToUpdate = Mapper.Map<TModel>(viewModel);
            var result = await Service.Update(modelToUpdate);
            await ProcessResult(result);
        }
    }
}
