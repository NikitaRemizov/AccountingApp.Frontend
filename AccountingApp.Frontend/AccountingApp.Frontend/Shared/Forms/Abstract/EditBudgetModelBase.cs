using AccountingApp.Frontend.Services.Models;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class EditBudgetModelBase<TModel> : BudgetModelComponentBase<TModel> where TModel : BudgetModel, new()
    {
        [Parameter]
        public TModel Model { get; set; }

        protected override async Task HandleUserSubmit()
        {
            var modelToCreate = Mapper.Map<TModel>(Model);
            var result = await Service.Update(modelToCreate);
            await ProcessResult(result, "The record is not edited");

            Model = new TModel();
            MudDialog.Close();
        }

    }
}
