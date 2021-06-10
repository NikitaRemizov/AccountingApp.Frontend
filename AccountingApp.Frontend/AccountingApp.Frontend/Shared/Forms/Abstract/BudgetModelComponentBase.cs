using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.Models;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Shared.Models;
using AutoMapper;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Shared.Forms.Abstract
{
    public abstract class BudgetModelComponentBase<TModel, TView> : DataAccessComponent where TModel : BudgetModel where TView : BudgetViewModel, new()
    {        
        protected bool IsValidationSuccessful { get; set; }

        [CascadingParameter]
        protected MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public virtual IRepository<TModel> Repository
        {
            get { return _repository ?? throw new ArgumentNullException(); }
            set { _repository = value; }
        }

        [Inject]
        protected IAccountService Account { get; set; }
        [Inject]
        protected IMapper Mapper { get; set; }

        private IRepository<TModel> _repository;

        protected abstract Task HandleUserSubmit();
    }
}
