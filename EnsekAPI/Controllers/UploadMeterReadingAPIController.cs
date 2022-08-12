using System;
using EnsekAPI.DataRepository;
using EnsekAPI.Helpers;
using EnsekAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnsekAPI.Controllers
{
    /// <summary>
    /// The upload meter reading api controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UploadMeterReadingAPIController : ControllerBase
    {
        private readonly ICsvImporter _csvImporter;
        private readonly IMeterReadingSqlContext _meterReadingSqlContext;
        private readonly ILogger<UploadMeterReadingAPIController> _logger;

        /// <summary>
        /// The upload meter reading api controller.
        /// </summary>
        /// <param name="logger">The logger</param>
        /// <param name="meterReadingSqlContext">The meter reading sql context.</param>
        /// <param name="CsvImport">The csv importer.</param>
        public UploadMeterReadingAPIController(ILogger<UploadMeterReadingAPIController> logger, IMeterReadingSqlContext meterReadingSqlContext, ICsvImporter csvImporter)
        {
            _logger = logger;
            _csvImporter = csvImporter;
            _meterReadingSqlContext = meterReadingSqlContext;
        }

        /// <summary>
        /// Post csv file.
        /// </summary>
        /// <param name="file">CSV file.</param>
        /// <returns>Upload result</returns>
        [HttpPost]
        [Route("meter-reading-uploads")]
        public IActionResult Post(IFormFile file)
        {
            _logger.LogInformation("File received. Processing.");

            var meterReadings = _csvImporter.ConvertCsvFileToMeterReadings(file);

            _meterReadingSqlContext.InsertMeterReadings(meterReadings);

            var result = new MeterReadingsUploadResult()
            {
                SuccessfulReadingsCount = _meterReadingSqlContext.NoOfValidRecordsCount,
                FailedReadingsCount = _meterReadingSqlContext.NoOfInvalidRecordsCount + _csvImporter.InvalidRecordsCount

            };

            return Ok(result);
        }
    }
}

