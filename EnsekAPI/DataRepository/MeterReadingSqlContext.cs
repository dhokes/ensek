using System;
using EnsekAPI.Helpers;
using EnsekAPI.Models;

namespace EnsekAPI.DataRepository
{
    /// <summary>
    /// Meter reading sql context.
    /// </summary>
	public class MeterReadingSqlContext : IMeterReadingSqlContext
    {
        private readonly ILogger<MeterReadingSqlContext> _logger;
        private readonly IValidationHelper _validationHelper;
        private readonly DatabaseContext _dbContext;

        /// <summary>
        /// Meter reading sql context.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="validationHelper">The validation helper.</param>
        /// <param name="context">The database context.</param>
        public MeterReadingSqlContext(ILogger<MeterReadingSqlContext> logger, IValidationHelper validationHelper, DatabaseContext databaseContext)
        {
            _logger = logger;
            _validationHelper = validationHelper;
            _dbContext = databaseContext;
        }

        public int NoOfInvalidRecordsCount { get; set; }
        public int NoOfValidRecordsCount { get; set; }

        /// <summary>
        /// Insert meter readings.
        /// </summary>
        /// <param name="meterReadings">A list of meter readings.</param>
        public void InsertMeterReadings(List<MeterReading> meterReadings)
        {
            using (_dbContext)
            {
                var existingAccountIds = _dbContext.Accounts.Select(x => x.Id).ToList();

                foreach (var reading in meterReadings)
                {
                    if (_validationHelper.AccountExists(existingAccountIds, reading) &&
                        _validationHelper.IsUniqueMeterReading(_dbContext.MeterReadings, reading) &&
                        _validationHelper.IsNewerMeterReading(_dbContext.MeterReadings, reading))
                    {
                        try
                        {
                            _dbContext.MeterReadings.Add(reading);
                            NoOfValidRecordsCount += 1;
                        }
                        catch (Exception e)
                        {
                            _logger.LogError($"Exception when attempting to save meter reading. {e}.");
                            NoOfInvalidRecordsCount += 1;
                        }
                    }
                    else
                    {
                        NoOfInvalidRecordsCount += 1;
                    }

                }

                _dbContext.SaveChanges();
            }
        }
    }
}