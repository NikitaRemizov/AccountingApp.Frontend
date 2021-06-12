using System;

namespace AccountingApp.Frontend.Services.Models
{
    public class BudgetChange : BudgetModel
    {        
        public DateTime? Date { get; set; }
        public double AmountInDollars { get; set; }
        public BudgetType BudgetType { get; set; }
    }
}
