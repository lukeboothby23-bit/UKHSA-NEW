namespace App.Models;
using System.ComponentModel.DataAnnotations;

public class ApproveRequestDto
{
    public int Id { get; set; }
    [Required]
    public required string Title {get; set;}
    [Required]
    public required string Username {get; set;}
    public string? Reason {get; set;}
    [Required]
    public required DateTime Timestamp {get; set;}
}