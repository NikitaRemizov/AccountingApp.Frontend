using AccountingApp.Frontend.Models;
using AccountingApp.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class EditBudgetModelBase<TModel, TView> : BudgetModelComponentBase<TModel, TView> where TModel : BudgetModel where TView : BudgetViewModel, new()
    {
        [Parameter]
        public TView ViewModel { get; set; }

        protected override async Task HandleUserSubmit()
        {
            var modelToCreate = Mapper.Map<TModel>(ViewModel);
            var result = await Repository.Update(modelToCreate);
            await ProcessResult(result, "The record is not edited");

            ViewModel = new TView();
            MudDialog.Close();
        }

    }
}
