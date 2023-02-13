using System.Globalization;

namespace ShiftsDomain
{
    public static class DateTimeExtension
    {
        public static string NameRus(this DateTime date) => date.ToString(string.Format("MMMM, yyyy"), CultureInfo.GetCultureInfo("ru-RU"));        
        public static string NameEN(this DateTime date) => date.ToString(string.Format("yyyy_MMMM"), CultureInfo.GetCultureInfo("en-EN"));               
        public static DateTime FirstDateInMonth(this DateTime date) => new DateTime(date.Year, date.Month, 1);
        public static DateTime LastDateInMonth(this DateTime date) => new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

    }
}
