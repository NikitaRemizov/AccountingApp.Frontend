using AccountingApp.Frontend.DataAccess.Repositories;
using AccountingApp.Shared.Models;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace AccountingApp.Frontend.Tests.DataAccess.RepositoryTests
{
    public class BudgetTypesTests : BudgetModelsTests<BudgetType>
    {
        protected override BudgetTypes Repository { get; }

        public BudgetTypesTests()
        {
            Repository = new BudgetTypes(Client, ApiEndpoints);
        }

        [Fact]
        public void GetAll_CallMethod_ClientGetCalledOnce()
        {
            SetupMockGet();

            Repository.GetAll().Wait();

            MockVerifyGetCalledOnce();
        }
    }
}
