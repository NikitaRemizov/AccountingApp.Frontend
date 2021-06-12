using AccountingApp.Frontend.Services.Models;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class AccountingApiResultExtensions
    {
        public static string ToPageMessage(this ServiceResult result) 
            => result switch
        {
            ServiceResult.Ok => "Operation successful",
            ServiceResult.Unauthorized => "Before accessing this page please login",
            ServiceResult.Error => "An error occured, while performing operation",
            _ => "Error"
        };
    }
}
