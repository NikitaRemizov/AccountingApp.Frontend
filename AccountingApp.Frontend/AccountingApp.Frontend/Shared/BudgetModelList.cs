using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Frontend.Models;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Utils.Extensions;
using AccountingApp.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared
{
    public abstract class BudgetModelList<TModel, TView> : DataAccessComponent where TModel : BudgetModel where TView : BudgetViewModel, new()
    {
        protected List<TView> ViewModelItems { get; set; } = new List<TView>();

        protected abstract IRepository<TModel> Repository { get; }

        [Inject]
        protected IAccountService Account { get; set; }
        [Inject]
        protected IMapper Mapper { get; set; }

        protected abstract Task InitializeItemsList();

        protected override async Task OnInitializedAsync()
        {
            if (!(await Account.TrySetAccessToken(Repository)))
            {
                NavigationManager.NavigateToAuthentification();
                return;
            }

            await InitializeItemsList();
        }

        protected async Task Delete(TView viewModel)
        {
            var id = viewModel?.Id;
            if (id is null || id == Guid.Empty)
            {
                return;
            }

            var result = await Repository.Delete((Guid)id);
            await ProcessResult(result);
            if (result != AccountingApiResult.Ok)
            {
                return;
            }

            ViewModelItems.Remove(viewModel);
            StateHasChanged();
        }

        protected async Task Update(TView viewModel)
        {
            var id = viewModel?.Id;
            if (id is null || id == Guid.Empty)
            {
                return;
            }
            var modelToUpdate = Mapper.Map<TModel>(viewModel);
            var result = await Repository.Update(modelToUpdate);
            await ProcessResult(result);
        }
    }
}
