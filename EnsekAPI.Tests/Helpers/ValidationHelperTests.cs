using System;
using EnsekAPI.Helpers;
using EnsekAPI.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace EnsekAPI.Tests.Helpers
{
    [TestClass]
    public class ValidationHelperTests
    {
        [TestMethod]
        public void IsValidMeterReading()
        {
            //Arrange
            var meterReading = "5";

            //Act
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsValidMeterReading(meterReading);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsValidMeterReading_EmptyString_Returns_False()
        {
            //Arrange
            var meterReading = string.Empty;

            //Act
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsValidMeterReading(meterReading);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsValidMeterReading_GreaterThan5Characters_Returns_False()
        {
            //Arrange
            var meterReading = "123456";

            //Act
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsValidMeterReading(meterReading);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsValidMeterReading_String_Returns_False()
        {
            //Arrange
            var meterReading = "errorString";

            //Act
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsValidMeterReading(meterReading);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void AccountExists_ExistingAccount_Returns_True()
        {
            //Arrange
            var existingAccountIds = new List<int> { 1, 2, 3, 4, 5 };

            //Act
            var meterReading = new MeterReading() { AccountId = 4 };
            var validationHelper = new ValidationHelper();
            var result = validationHelper.AccountExists(existingAccountIds, meterReading);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void AccountExists_NewAccount_Returns_False()
        {
            //Arrange
            var existingAccountIds = new List<int> { 1, 2, 3, 4, 5 };

            //Act
            var meterReading = new MeterReading() { AccountId = 34 };
            var validationHelper = new ValidationHelper();
            var result = validationHelper.AccountExists(existingAccountIds, meterReading);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsUniqueMeterReading_UniqueReading_Returns_True()
        {
            //Arrange
            var data = new List<MeterReading>
            {
                new MeterReading { AccountId = 12, MeterReadingDateTime = new DateTime(2022, 1, 3), ReadingValue = "24" },
                new MeterReading { AccountId = 32, MeterReadingDateTime = new DateTime(2022, 4, 2), ReadingValue = "11"  },
                new MeterReading { AccountId = 5, MeterReadingDateTime = new DateTime(2022, 5, 6), ReadingValue = "13"  },
            }.AsQueryable();

            var dbSet = new Mock<DbSet<MeterReading>>();
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            //Act
            var meterReading = new MeterReading() { AccountId = 34, ReadingValue = "552", MeterReadingDateTime = new DateTime(2022, 4, 4) };
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsUniqueMeterReading(dbSet.Object, meterReading);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsUniqueMeterReading_DuplicateReading_Returns_False()
        {
            //Arrange
            var meterReading = new MeterReading() { AccountId = 34, ReadingValue = "552", MeterReadingDateTime = new DateTime(2022, 4, 4) };

            var data = new List<MeterReading>
            {
                new MeterReading { AccountId = 12, MeterReadingDateTime = new DateTime(2022, 1, 3), ReadingValue = "24" },
                meterReading,
                new MeterReading { AccountId = 5, MeterReadingDateTime = new DateTime(2022, 5, 6), ReadingValue = "13"  },
            }.AsQueryable();

            var dbSet = new Mock<DbSet<MeterReading>>();
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            //Act
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsUniqueMeterReading(dbSet.Object, meterReading);

            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void IsNewerMeterReading_NewerReading_Returns_True()
        {
            //Arrange
            var data = new List<MeterReading>
            {
                new MeterReading { AccountId = 12, MeterReadingDateTime = new DateTime(2022, 1, 3), ReadingValue = "24" },
                new MeterReading { AccountId = 32, MeterReadingDateTime = new DateTime(2022, 4, 2), ReadingValue = "11"  },
                new MeterReading { AccountId = 5, MeterReadingDateTime = new DateTime(2022, 5, 6), ReadingValue = "13"  },
            }.AsQueryable();

            var dbSet = new Mock<DbSet<MeterReading>>();
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            //Act
            var meterReading = new MeterReading() { AccountId = 5, ReadingValue = "20", MeterReadingDateTime = new DateTime(2022, 5, 7) };
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsNewerMeterReading(dbSet.Object, meterReading);

            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void IsNewerMeterReading_OlderReading_Returns_False()
        {
            //Arrange
            var data = new List<MeterReading>
            {
                new MeterReading { AccountId = 12, MeterReadingDateTime = new DateTime(2022, 1, 3), ReadingValue = "24" },
                new MeterReading { AccountId = 32, MeterReadingDateTime = new DateTime(2022, 4, 2), ReadingValue = "11"  },
                new MeterReading { AccountId = 5, MeterReadingDateTime = new DateTime(2022, 5, 6), ReadingValue = "13"  },
            }.AsQueryable();

            var dbSet = new Mock<DbSet<MeterReading>>();
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Provider).Returns(data.Provider);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.Expression).Returns(data.Expression);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.ElementType).Returns(data.ElementType);
            dbSet.As<IQueryable<MeterReading>>().Setup(m => m.GetEnumerator()).Returns(() => data.GetEnumerator());

            //Act
            var meterReading = new MeterReading() { AccountId = 5, ReadingValue = "20", MeterReadingDateTime = new DateTime(2022, 5, 3) };
            var validationHelper = new ValidationHelper();
            var result = validationHelper.IsNewerMeterReading(dbSet.Object, meterReading);

            //Assert
            Assert.AreEqual(false, result);
        }
    }
}

