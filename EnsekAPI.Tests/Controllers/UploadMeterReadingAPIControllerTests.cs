using System;
using System.Text;
using EnsekAPI.Controllers;
using EnsekAPI.DataRepository;
using EnsekAPI.Helpers;
using EnsekAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace EnsekAPI.Tests.Controllers
{
    [TestClass]
    public class UploadMeterReadingAPIControllerTests
    {
        [TestMethod]
        public void Post_SuccessfullyReturns200StatusCode()
        {
            //Arrange
            var noOfInvalidRecordsCountFromSqlContext = 5;
            var noOfValidRecordsCountsFromSqlContext = 24;
            var noOfInvalidRecordsCountsFromCSVImporter = 2;

            var loggerMock = new Mock<ILogger<UploadMeterReadingAPIController>>();
            var meterReadingSqlContextMock = new Mock<IMeterReadingSqlContext>();
            meterReadingSqlContextMock.Setup(x => x.NoOfInvalidRecordsCount).Returns(noOfInvalidRecordsCountFromSqlContext);
            meterReadingSqlContextMock.Setup(x => x.NoOfValidRecordsCount).Returns(noOfValidRecordsCountsFromSqlContext);

            var csvImporterMock = new Mock<ICsvImporter>();
            csvImporterMock.Setup(x => x.ConvertCsvFileToMeterReadings(It.IsAny<FormFile>())).Returns(new List<MeterReading>());
            csvImporterMock.Setup(x => x.InvalidRecordsCount).Returns(noOfInvalidRecordsCountsFromCSVImporter);

            //Act
            var controller = new UploadMeterReadingAPIController(loggerMock.Object, meterReadingSqlContextMock.Object, csvImporterMock.Object);

            byte[] bytes = Encoding.UTF8.GetBytes("AccountId,MeterReadingDateTime,MeterReadValue" +
                Encoding.ASCII.GetBytes(Environment.NewLine) +
                "1234,01/05/2022 19:12,01111" +
                Encoding.ASCII.GetBytes(Environment.NewLine) +
                "3344,04/05/2022 11:42,32222");

            var file = new FormFile(
                baseStream: new MemoryStream(bytes),
                baseStreamOffset: 0,
                length: bytes.Length,
                name: "Data",
                fileName: "dummy.csv"
            );

            var actionResult = controller.Post(file);
            var objectResult = actionResult as OkObjectResult;
            var uploadResult = objectResult.Value as MeterReadingsUploadResult;

            //Assert
            Assert.AreEqual(200, objectResult.StatusCode);
            Assert.AreEqual(noOfInvalidRecordsCountFromSqlContext + noOfInvalidRecordsCountsFromCSVImporter, uploadResult.FailedReadingsCount);
            Assert.AreEqual(noOfValidRecordsCountsFromSqlContext, uploadResult.SuccessfulReadingsCount);
        }
    }
}