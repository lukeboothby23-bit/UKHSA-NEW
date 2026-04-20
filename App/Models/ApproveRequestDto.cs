namespace App.Models;
using System.ComponentModel.DataAnnotations;

public class ApproveRequestDto
{
    public int User{ get; set; }
    public int Id { get; set; }
    [Required]
    public required string Title {get; set;}
    [Required]
    public required string Username {get; set;}
    [Required]
    public required DateTime Timestamp {get; set;}
}