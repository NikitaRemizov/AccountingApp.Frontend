using AccountingApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AccountingApp.Frontend.Tests.Utils
{
    public class BudgetChangeComparer : IEqualityComparer<BudgetChange>
    {
        public bool Equals(BudgetChange b1, BudgetChange b2)
        {
            return b1.Id == b2.Id &&
                   b1.Date == b2.Date &&
                   b1.Amount == b2.Amount &&
                   b1.BudgetTypeId == b2.BudgetTypeId &&
                   b1.BudgetTypeName == b2.BudgetTypeName;
        }

        public int GetHashCode([DisallowNull] BudgetChange b)
        {
            return HashCode.Combine(b.Id, b.Date, b.Amount, b.BudgetTypeId, b.BudgetTypeName);
        }
    }
}
