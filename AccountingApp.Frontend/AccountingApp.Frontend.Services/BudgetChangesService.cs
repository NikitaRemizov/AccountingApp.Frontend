using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services
{
    public class BudgetChangesService 
        : BudgetModelsService<BudgetChange, AccountingApp.Shared.Models.BudgetChange>, IBudgetChangesService
    {
        protected override IBudgetChangeRepository Repository { get; }

        protected override IMapper Mapper { get; }

        public BudgetChangesService(IMapper mapper, IBudgetChangeRepository repository)
        {
            Repository = repository;
            Mapper = mapper;
        }

        public async Task<(IEnumerable<BudgetChange>, ServiceResult)> GetBetweenDates(DateTime from, DateTime to)
        {
            var (budgetChanges, result) = await Repository.GetBetweenDates(from, to);
            return (
                Mapper.Map<IEnumerable<BudgetChange>>(budgetChanges),
                Mapper.Map<ServiceResult>(result)
            );
        }

        public async Task<(IEnumerable<BudgetChange>, ServiceResult)> GetForDate(DateTime date)
        {
            var (budgetChanges, result) = await Repository.GetForDate(date);
            return (
                Mapper.Map<IEnumerable<BudgetChange>>(budgetChanges),
                Mapper.Map<ServiceResult>(result)
            );
        }
    }
}
