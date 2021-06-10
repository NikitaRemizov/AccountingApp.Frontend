using System;

namespace AccountingApp.Frontend.Models
{
    public class BudgetChangeViewModel : BudgetViewModel
    {        
        public DateTime? Date { get; set; }
        public double AmountInDollars { get; set; }
        public BudgetTypeViewModel BudgetType { get; set; }
    }
}
