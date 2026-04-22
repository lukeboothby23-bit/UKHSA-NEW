namespace UKHSA.Models;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
    // NOTE: Id field is inherited from IdentityUser

    public required string Forename { get; set; }
    public required string Surname { get; set; }

    public List<Request> Requests { get; set; } = [];
}

public static class Roles {
    public const string AdminRole = "Admin";
    public const string ApproverRole = "Approver";
    public const string UserRole = "User";
}

public class Request
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }

    public required string UserId { get; set; }
    public User? User { get; set; }

    public int DatasetId { get; set; }
    public Dataset? Dataset { get; set; }

    public Approval? Approval { get; set; }
}

public class Dataset
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public int AccessLevel { get; set; }

    public List<Request> Requests { get; set; } = [];
 }

public class Approval
{
    public int Id { get; set; }
    public bool Approved { get; set; }
    public DateTime Timestamp { get; set; }
    public DateTime Expires { get; set; }
    public required string RejectedReason { get; set; }

    public int RequestId { get; set; }
    [ForeignKey(nameof(RequestId))]
    public Request? Request { get; set; }
}
