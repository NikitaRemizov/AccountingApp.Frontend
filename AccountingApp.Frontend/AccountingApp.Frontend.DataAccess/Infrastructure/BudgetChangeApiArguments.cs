namespace AccountingApp.Frontend.DataAccess.Infrastructure
{
    public class BudgetChangeApiArguments
    {
        public string SingleDate { get; init; }
        public string FromDate { get; init; }
        public string ToDate { get; init; }
    }
}