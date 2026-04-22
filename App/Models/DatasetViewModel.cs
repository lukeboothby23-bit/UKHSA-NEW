namespace UKHSA.Models;
using System.ComponentModel.DataAnnotations;

public class AddDatasetViewModel
{
	[Required]
	public required string Title { get; set; }

	[Required]
	public required string Description { get; set; }

	[Required]
	public required string AccessLevel { get; set; }
}
