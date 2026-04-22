namespace UKHSA.Models;
using System.ComponentModel.DataAnnotations;

public class RequestsDto
{
    [Required]
    public required string Title {get; set;}
    [Required]
    public required bool Approved {get; set;}
    [Required]
    public required string Reason {get; set;}
    [Required]
    public required DateTime ReqTime {get; set;}
    [Required]
    public required string AppTime {get; set;}
    [Required]
    public required string AppExp {get; set;}
    public required string ViewDataset {get; set;}
}