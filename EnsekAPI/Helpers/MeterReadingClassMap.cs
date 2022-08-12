using System;
using CsvHelper.Configuration;
using EnsekAPI.Models;

namespace EnsekAPI.Helpers
{
    public class MeterReadingClassMap : ClassMap<MeterReading>
    {
        public MeterReadingClassMap()
        {
            Map(m => m.AccountId).Name("AccountId");
            Map(m => m.MeterReadingDateTimeAsString).Name("MeterReadingDateTime");
            Map(m => m.ReadingValue).Name("MeterReadValue");
        }
    }
}