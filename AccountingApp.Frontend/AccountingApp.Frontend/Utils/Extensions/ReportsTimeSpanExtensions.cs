
using AccountingApp.Frontend.Models;
using System;

namespace AccountingApp.Frontend.Utils.Extensions
{
    public static class ReportTimeSpanExtensions
    {
        public static DateTime ToLowerBoundDate(this ReportTimeSpan reportTimeSpan)
        {
            var today = DateTime.Today;
            return reportTimeSpan switch
            {
                ReportTimeSpan.Day => today.AddDays(-1),
                ReportTimeSpan.Month => today.AddMonths(-1),
                _ => throw new ArgumentOutOfRangeException(
                    $"The provided value {nameof(reportTimeSpan)}={reportTimeSpan} is unsupported")
            };
        }
    }
}
