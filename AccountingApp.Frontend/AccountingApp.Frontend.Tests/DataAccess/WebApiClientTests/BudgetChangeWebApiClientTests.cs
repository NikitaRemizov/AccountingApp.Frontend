using AccountingApp.Frontend.Tests.Utils;
using AccountingApp.Shared.Models;
using System.Collections.Generic;

namespace AccountingApp.Frontend.Tests.DataAccess.WebApiClientTests
{
    public class BudgetChangeWebApiClientTests : WebApiClientTests<BudgetChange>
    {
        protected override IEqualityComparer<BudgetChange> Comparer { get; } = new BudgetChangeComparer();
    }
}
