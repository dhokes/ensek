using System;
using System.Globalization;

namespace EnsekAPI.Extensions
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Pad meter reading to 5 characters if required.
        /// </summary>
        /// <param name="meterReading">Meter reading</param>
        /// <returns>Padded meter reading</returns>
        public static string PadMeterReadingIfRequired(this string meterReading)
        {
            return meterReading.PadLeft(5, '0');
        }

        /// <summary>
        /// Convert string to a datetime object.
        /// </summary>
        /// <param name="dateTimeString">Datetime as string.</param>
        /// <returns>Datetime object</returns>
        public static DateTime ToDateTime(this string dateTimeString)
        {
            return DateTime.ParseExact(dateTimeString, "dd/MM/yyyy HH:m", CultureInfo.InvariantCulture);
        }
    }
}