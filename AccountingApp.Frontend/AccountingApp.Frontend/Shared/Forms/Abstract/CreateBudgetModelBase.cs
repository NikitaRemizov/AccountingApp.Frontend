using AccountingApp.Frontend.Services.Models;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class CreateBudgetModelBase<TModel> : BudgetModelComponentBase<TModel> where TModel : BudgetModel, new()
    {
        protected TModel Model { get; set; } = new TModel();

        protected override async Task HandleUserSubmit()
        {
            var modelToCreate = Mapper.Map<TModel>(Model);
            var (_, result) = await Service.Create(modelToCreate);
            await ProcessResult(result, "The record is not created");

            Model = new TModel();
            MudDialog.Close();
        }
    }
}
