using System;
using EnsekAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsekAPI.Helpers
{
	public class ValidationHelper : IValidationHelper
    {
        public bool IsValidMeterReading(string meterReading)
        {
            return IsValidLength(meterReading) && IsParseableAsInt(meterReading);
        }

        public bool AccountExists(List<int> existingAccountIds, MeterReading reading)
        {
            return existingAccountIds.Contains(reading.AccountId);
        }

        public bool IsUniqueMeterReading(DbSet<MeterReading> dbMeterReadings, MeterReading reading)
        {
            var isUniqueMeterReading = !dbMeterReadings.Any(x => x.AccountId == reading.AccountId &&
                                            x.ReadingValue == reading.ReadingValue &&
                                            x.MeterReadingDateTime == reading.MeterReadingDateTime);

            return isUniqueMeterReading;
        }

        public bool IsNewerMeterReading(DbSet<MeterReading> dbMeterReadings, MeterReading reading)
        {
            var latestMeterReading = dbMeterReadings.OrderByDescending(x => x.AccountId == reading.AccountId).FirstOrDefault();

            if (latestMeterReading == null)
                return true;
            else
            {
                if (reading.MeterReadingDateTime > latestMeterReading.MeterReadingDateTime)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// Check to see if a meter reading is of valid length.
        /// </summary>
        /// <param name="meterReading">A meter reading.</param>
        /// <returns>True, if valid.</returns>
        private bool IsValidLength(string meterReading)
        {
            if (string.IsNullOrWhiteSpace(meterReading))
            {
                return false;
            }

            return meterReading.Length < 6;
        }

        /// <summary>
        /// Check to see if meter reading is parseable as an integer.
        /// </summary>
        /// <param name="meterReading">A meter reading.</param>
        /// <returns>True if parseable as an integer.</returns>
        private bool IsParseableAsInt(string meterReading)
        {
            var number = 0;
            return int.TryParse(meterReading, out number);
        }
    }
}

