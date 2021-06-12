using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class BudgetModelComponentBase<TModel> : DataAccessComponent where TModel : BudgetModel
    {        
        protected bool IsValidationSuccessful { get; set; }

        [CascadingParameter]
        protected MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public virtual IBudgetModelsService<TModel> Service
        {
            get { return _service ?? throw new ArgumentNullException(); }
            set { _service = value; }
        }

        [Inject]
        protected IMapper Mapper { get; set; }

        private IBudgetModelsService<TModel> _service;

        protected abstract Task HandleUserSubmit();
    }
}
