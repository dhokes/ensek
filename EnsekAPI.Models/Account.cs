using System.ComponentModel.DataAnnotations;

namespace EnsekAPI.Models;

public class Account
{
    [Key]
    [Required]
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }
}