using System;

namespace AccountingApp.Shared.Converters
{
    public static class DollarsToCents
    {
        public static long Convert(double dollars)
        {
            return System.Convert.ToInt64(Math.Floor(dollars * 100.0));
        }

        public static double ConvertBack(long cents)
        {
            return cents / 100.0;
        }
    }
}
