namespace AccountingApp.Frontend.Models
{
    public class BudgetReport
    {
        public string BudgetTypeName { get; set; }
        public long Amount { get; set; }
        public double AmountInDollars => Amount / 100.0;
    }
}
