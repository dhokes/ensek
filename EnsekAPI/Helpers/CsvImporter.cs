using System;
using CsvHelper;
using EnsekAPI.Models;
using System.Globalization;
using EnsekAPI.Extensions;

namespace EnsekAPI.Helpers
{
    /// <summary>
    /// CSV Importer
    /// </summary>
    public class CsvImporter : ICsvImporter
    {
        private readonly IValidationHelper _validationHelper;
        private readonly ILogger<CsvImporter> _logger;

        public CsvImporter(ILogger<CsvImporter> logger, IValidationHelper validationHelper)
        {
            _logger = logger;
            _validationHelper = validationHelper;
        }

        public int InvalidRecordsCount { get; set; }

        public List<MeterReading> ConvertCsvFileToMeterReadings(IFormFile file)
        {
            var meterReadings = new List<MeterReading>();

            using (var csvReader = new CsvReader(new StreamReader(file.OpenReadStream()), CultureInfo.InvariantCulture))
            {
                while (csvReader.Read())
                {
                    try
                    {
                        csvReader.Context.RegisterClassMap<MeterReadingClassMap>();

                        var meterRecord = csvReader.GetRecord<MeterReading>();

                        if (_validationHelper.IsValidMeterReading(meterRecord.ReadingValue))
                        {
                            meterRecord.ReadingValue = meterRecord.ReadingValue.PadMeterReadingIfRequired();
                            meterRecord.MeterReadingDateTime = meterRecord.MeterReadingDateTimeAsString.ToDateTime();

                            meterReadings.Add(meterRecord);
                        }
                        else
                        {
                            InvalidRecordsCount += 1;
                        }

                    }
                    catch (CsvHelperException e)
                    {
                        _logger.LogError($"Error when attempting to parse a csv file. {e}.");
                        InvalidRecordsCount += 1;
                    }
                }
            }

            return meterReadings;
        }
    }
}