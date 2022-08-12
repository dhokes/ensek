using EnsekAPI.Models;

namespace EnsekAPI.Helpers
{
    /// <summary>
    /// CSV Importer interface
    /// </summary>
    public interface ICsvImporter
    {
        /// <summary>
        /// The number of invalid records in a csv file.
        /// </summary>
        int InvalidRecordsCount { get; set; }

        /// <summary>
        /// Convert csv file to a list of meter readings.
        /// </summary>
        /// <param name="file">File</param>
        /// <returns>A list of meter readings.</returns>
        List<MeterReading> ConvertCsvFileToMeterReadings(IFormFile file);
    }
}