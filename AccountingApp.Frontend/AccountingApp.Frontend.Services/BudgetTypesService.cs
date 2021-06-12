using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services
{
    public class BudgetTypesService 
        : BudgetModelsService<BudgetType, AccountingApp.Shared.Models.BudgetType>, IBudgetTypesService
    {
        protected override IBudgetTypeRepository Repository { get; }
        protected override IMapper Mapper { get; }

        public BudgetTypesService(IMapper mapper, IBudgetTypeRepository repository)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<(IEnumerable<BudgetType>, ServiceResult)> GetAll()
        {
            var (budgetTypes, result) = await Repository.GetAll();
            return (
                Mapper.Map<IEnumerable<BudgetType>>(budgetTypes),
                Mapper.Map<ServiceResult>(result)
            );
        }
    }
}
