﻿using AccountingApp.Frontend.DataAccess.Repositories.Interfaces;
using AccountingApp.Frontend.DataAccess.Utils;
using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace AccountingApp.Frontend.DataAccess.Repositories
{
    public class BudgetChangeRepository : BudgetModelRepository<BudgetChange>, IBudgetChangeRepository
    {
        public BudgetChangeRepository(HttpClient client)
            : base(client, "budget/change")
        {
        }

        public async Task<(IEnumerable<BudgetChange>, DataAccessResult)> GetBetweenDates(DateTime from, DateTime to)
        {
            var requestUrl = $"{_endpointPath}/betweendates" + 
                $"/?from={DateToString(from)}&to={DateToString(to)}";

            return await Client.Get<List<BudgetChange>>(requestUrl);
        }

        public async Task<(IEnumerable<BudgetChange>, DataAccessResult)> GetForDate(DateTime date)
        {
            var requestUrl = $"{_endpointPath}/fordate" + 
                $"/?date={DateToString(date)}";

            return await Client.Get<List<BudgetChange>>(requestUrl);
        }

        private static string DateToString(DateTime date)
        {
            return date.ToString("MM-dd-yyyy");
        }
    }
}
