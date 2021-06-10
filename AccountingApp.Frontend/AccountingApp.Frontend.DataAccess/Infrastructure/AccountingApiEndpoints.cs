namespace AccountingApp.Frontend.DataAccess.Infrastructure
{
    public class AccountingApiEndpoints
    {
        public ApiEndpoint Login { get; init; }
        public ApiEndpoint Register { get; init; }
        public ApiEndpoint BudgetType { get; init; }
        public BudgetChangeEndpoint BudgetChange { get; init; }
    }
}