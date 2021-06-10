using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AccountingApp.Frontend.Tests.Utils
{
    public class BudgetTypeComparer : IEqualityComparer<BudgetType>
    {
        public bool Equals(BudgetType b1, BudgetType b2)
        {
            return b1.Id == b2.Id &&
                   b1.Name == b2.Name;
        }

        public int GetHashCode([DisallowNull] BudgetType b)
        {
            return HashCode.Combine(b.Id, b.Name);
        }
    }
}
