using AccountingApp.Frontend.DataAccess.Utils;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class AccountingApiResultExtensions
    {
        public static string ToPageMessage(this AccountingApiResult result) 
            => result switch
        {
            AccountingApiResult.Ok => "Operation successful",
            AccountingApiResult.Unauthorized => "Before accessing this page please login",
            AccountingApiResult.Error => "An error occured, while performing operation",
            AccountingApiResult.ServerUnreachable => "Error while connecting to server",
            _ => "Error"
        };
    }
}
