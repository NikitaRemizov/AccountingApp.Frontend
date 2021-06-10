namespace AccountingApp.Frontend.DataAccess.Infrastructure
{
    public class BudgetChangeEndpoint : ApiEndpoint
    {
        public BudgetChangeApiArguments Arguments { get; init; }
        public string PathForDate { get; init; }
        public string PathBetweenDates { get; init; }
    }
}
