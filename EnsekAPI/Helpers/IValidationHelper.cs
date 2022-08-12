using EnsekAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EnsekAPI.Helpers
{
    /// <summary>
    /// Validation helper interface.
    /// </summary>
    public interface IValidationHelper
    {
        /// <summary>
        /// Check to see if the meter reading is for an existing account.
        /// </summary>
        /// <param name="existingAccountIds">A list of existing account ids.</param>
        /// <param name="reading">Meter reading</param>
        /// <returns>True if the meter reading is for an existing account.</returns>
        bool AccountExists(List<int> existingAccountIds, MeterReading reading);

        /// <summary>
        /// Check to see if meter reading is a valid meter reading.
        /// </summary>
        /// <param name="meterReading">The meter reading.</param>
        /// <returns>True, if valid meter reading.</returns>
        bool IsValidMeterReading(string meterReading);

        /// <summary>
        /// Check to see if the meter reading is unique.
        /// </summary>
        /// <param name="dbMeterReadings">A database set of meter readings.</param>
        /// <param name="reading">The meter reading.</param>
        /// <returns>True, if unique meter reading.</returns>
        bool IsUniqueMeterReading(DbSet<MeterReading> dbMeterReadings, MeterReading reading);

        /// <summary>
        /// Check to see if a meter reading is the latest one.
        /// </summary>
        /// <param name="dbMeterReadings">A database set of meter readings.</param>
        /// <param name="reading">A meter reading.</param>
        /// <returns>True, if newer meter reading.</returns>
        bool IsNewerMeterReading(DbSet<MeterReading> dbMeterReadings, MeterReading reading);
    }
}