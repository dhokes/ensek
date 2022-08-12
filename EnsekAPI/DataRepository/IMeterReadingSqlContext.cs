using EnsekAPI.Models;

namespace EnsekAPI.DataRepository
{
    /// <summary>
    /// Meter reading sql context.
    /// </summary>
    public interface IMeterReadingSqlContext
    {
        /// <summary>
        /// Insert a list of meter readings.
        /// </summary>
        /// <param name="meterReadings">A list of meter readings.</param>
        void InsertMeterReadings(List<MeterReading> meterReadings);

        /// <summary>
        /// THe number of invalid records.
        /// </summary>
        public int NoOfInvalidRecordsCount { get; set; }

        /// <summary>
        /// The number of valid records.
        /// </summary>
        public int NoOfValidRecordsCount { get; set; }
    }
}