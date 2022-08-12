using System;

namespace EnsekAPI.Models;

public class MeterReadingsUploadResult
{
    public int SuccessfulReadingsCount { get; set; }
    public int FailedReadingsCount { get; set; }
}