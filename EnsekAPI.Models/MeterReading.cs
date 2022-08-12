using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsekAPI.Models;

public class MeterReading
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Required]
    public string? ReadingValue { get; set; }

    [Required]
    public DateTime MeterReadingDateTime { get; set; }

    [NotMapped]
    public string? MeterReadingDateTimeAsString { get; set; }
}