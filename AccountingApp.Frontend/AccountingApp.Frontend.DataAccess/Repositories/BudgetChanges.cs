using AccountingApp.Frontend.DataAccess.Infrastructure;
using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public class BudgetChanges : BudgetModels<BudgetChange>, IBudgetChanges
    {
        private BudgetChangeEndpoint _endpoint;

        public BudgetChanges(WebApiClient<BudgetChange> client, AccountingApiEndpoints apiEndpoints)
            : base(client, apiEndpoints.BudgetChange.Path)
        {
            _endpoint = apiEndpoints.BudgetChange;
        }

        public async Task<(IEnumerable<BudgetChange>, AccountingApiResult)> GetBetweenDates(DateTime from, DateTime to)
        {
            var requestUrl = _endpoint.PathBetweenDates + 
                $"/?{_endpoint.Arguments.FromDate}={DateToString(from)}" +
                $"&{_endpoint.Arguments.ToDate}={DateToString(to)}";
            return await Client.Get<List<BudgetChange>>(requestUrl);
        }

        public async Task<(IEnumerable<BudgetChange>, AccountingApiResult)> GetForDate(DateTime date)
        {
            var requestUrl = _endpoint.PathForDate + 
                $"/?{_endpoint.Arguments.SingleDate}={DateToString(date)}";
            return await Client.Get<List<BudgetChange>>(requestUrl);
        }

        private static string DateToString(DateTime date)
        {
            return date.ToString("MM-dd-yyyy");
        }
    }
}
