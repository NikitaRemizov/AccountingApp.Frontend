using AccountingApp.Frontend.Tests.Utils;
using AccountingApp.Shared.Models;
using System.Collections.Generic;

namespace AccountingApp.Frontend.Tests.DataAccess.WebApiClientTests
{
    public class BudgetTypeWebApiClienTests : WebApiClientTests<BudgetType>
    {
        protected override IEqualityComparer<BudgetType> Comparer { get; } = new BudgetTypeComparer();
    }
}
