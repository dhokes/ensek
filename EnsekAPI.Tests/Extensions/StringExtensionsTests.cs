using System;
using EnsekAPI.Extensions;

namespace EnsekAPI.Tests.Extensions
{
    [TestClass]
    public class StringExtensionsTests
    {
        [TestMethod]
        public void PadMeterReadingIfRequired_Successfully()
        {
            //Arrange
            var meterReading = "4";

            //Act
            var result = meterReading.PadMeterReadingIfRequired();

            //Assert
            Assert.AreEqual("00004", result);
        }

        [TestMethod]
        public void PadMeterReadingIfRequired_Successfully_NoPaddingRequired()
        {
            //Arrange
            var meterReading = "12345";

            //Act
            var result = meterReading.PadMeterReadingIfRequired();

            //Assert
            Assert.AreEqual("12345", result);
        }

        [TestMethod]
        public void ToDateTime_Successfully()
        {
            //Arrange
            var dateTimeAsString = "04/05/2022 22:45";

            //Act
            var result = dateTimeAsString.ToDateTime();

            //Assert
            Assert.AreEqual(2022, result.Year);
            Assert.AreEqual(5, result.Month);
            Assert.AreEqual(4, result.Day);
            Assert.AreEqual(22, result.Hour);
            Assert.AreEqual(45, result.Minute);
        }
    }
}