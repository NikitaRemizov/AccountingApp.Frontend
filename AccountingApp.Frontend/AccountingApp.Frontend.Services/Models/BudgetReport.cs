namespace AccountingApp.Frontend.Services.Models
{
    public class BudgetReport
    {
        public string BudgetTypeName { get; set; }
        public long Amount { get; set; }
        // TODO: check if all fields are used
        public double AmountInDollars => Amount / 100.0;
    }
}
