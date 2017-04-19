using System;

namespace MT940toCSV
{
    /// <summary>
    /// Source: https://wiert.me/2014/04/22/in-c-given-a-datetime-object-how-do-i-get-a-iso8601-date-in-string-format-stack-overflow/
    /// </summary>
    public static class Iso8601DateTimeExtension
    {
        public static string ToIso8601DateOnly(this DateTime value)
        {
            return value.ToString("yyyy-MM-dd");
            // System.DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ssK");
        }
        public static string ToIso8601DateTime(this DateTime value)
        {
            return value.ToString("yyyy-MM-ddTHH:mm:ssK");
        }
    }
}