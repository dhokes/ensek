using System;
using EnsekAPI.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;

namespace EnsekAPI.Tests.Helpers
{
    [TestClass]
    public class CsvImporterTests
    {
        [TestMethod]
        public void ProcessCsv_Returns_Correct_Count()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<CsvImporter>>();

            var validationHelperMock = new Mock<IValidationHelper>();
            validationHelperMock.Setup(x => x.IsValidMeterReading(It.IsAny<string>())).Returns(true);

            var csvContent = new StringBuilder();
            csvContent.AppendLine("AccountId,MeterReadingDateTime,MeterReadValue");
            csvContent.AppendLine("1234,01/07/2022 12:05,1234");
            csvContent.AppendLine("1122,10/07/2022 14:30,1234");

            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write(csvContent);
            streamWriter.Flush();
            memoryStream.Position = 0;

            var csvFileMock = new Mock<IFormFile>();
            csvFileMock.Setup(x => x.OpenReadStream()).Returns(memoryStream);
            csvFileMock.Setup(x => x.FileName).Returns("test.csv");
            csvFileMock.Setup(x => x.Length).Returns(memoryStream.Length);

            var expecedTotalReadings = 2;

            //Act
            var csvImporter = new CsvImporter(loggerMock.Object, validationHelperMock.Object);
            var csvTotalReadings = csvImporter.ConvertCsvFileToMeterReadings(csvFileMock.Object);

            //Assert
            Assert.AreEqual(expecedTotalReadings, csvTotalReadings.Count());
        }
    }
}