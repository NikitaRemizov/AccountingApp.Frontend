using AccountingApp.Frontend.Tests.Utils;
using AccountingApp.Shared.Models;
using System.Collections.Generic;

namespace AccountingApp.Frontend.Tests.DataAccess.WebApiClientTests
{
    public class UserWebApiClientTests : WebApiClientTests<User>
    {
        protected override IEqualityComparer<User> Comparer { get; } = new UserComparer();
    }
}
