using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.Services.Interfaces;
using AccountingApp.Frontend.Services.Models;
using AutoMapper;
using System;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.Services
{
    public abstract class BudgetModelsService<TService, TDataAccess> 
        : IBudgetModelsService<TService> where TService : BudgetModel where TDataAccess : AccountingApp.Shared.Models.BudgetModel
    {
        protected abstract IRepository<TDataAccess> Repository { get; }

        protected abstract IMapper Mapper { get; }

        public virtual async Task<(Guid, ServiceResult)> Create(TService model)
        {
            var modelToCreate = Mapper.Map<TDataAccess>(model);
            var (id, result) = await Repository.Create(modelToCreate);
            return (id, Mapper.Map<ServiceResult>(result));
        }

        public virtual async Task<ServiceResult> Delete(Guid id)
        {
            var result = await Repository.Delete(id);
            return Mapper.Map<ServiceResult>(result);
        }

        public virtual async Task<ServiceResult> Update(TService model)
        {
            var modelToUpdate = Mapper.Map<TDataAccess>(model);
            var result = await Repository.Update(modelToUpdate);
            return Mapper.Map<ServiceResult>(result);
        }

        public virtual void SetAccessToken(string token)
        {
            Repository.SetAccessToken(
                new DataAccess.Models.AccessToken { Value = token }
            );
        }
    }
}
