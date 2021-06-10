using AccountingApp.Frontend.Models;
using AccountingApp.Shared.Models;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class CreateBudgetModelBase<TModel, TView> : BudgetModelComponentBase<TModel, TView> where TModel : BudgetModel where TView : BudgetViewModel, new()
    {
        protected TView ViewModel { get; set; } = new TView();

        protected override async Task HandleUserSubmit()
        {
            var modelToCreate = Mapper.Map<TModel>(ViewModel);
            var (_, result) = await Repository.Create(modelToCreate);
            await ProcessResult(result, "The record is not created");

            ViewModel = new TView();
            MudDialog.Close();
        }
    }
}
