using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Shared.Models;
using System;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public class BudgetChangesTests : BudgetModelsTests<BudgetChange>
    {
        protected override BudgetChanges Repository { get; }

        public BudgetChangesTests()
        {
            Repository = new BudgetChanges(Client, ApiEndpoints);
        }

        [Fact]
        public void GetBetweenDates_CallMethod_ClientGetCalledOnce()
        {
            SetupMockGet();

            Repository.GetBetweenDates(DateTime.Today, DateTime.Today).Wait();

            MockVerifyGetCalledOnce();
        }

        [Fact]
        public void GetForDate_CallMethod_ClientGetCalledOnce()
        {
            SetupMockGet();

            Repository.GetForDate(DateTime.Today).Wait();

            MockVerifyGetCalledOnce();
        }
    }
}
