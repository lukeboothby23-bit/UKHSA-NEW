namespace UKHSA.Controllers;

using UKHSA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class UKHSA_DbContext : IdentityDbContext<User>
{
    public required DbSet<Dataset> Datasets { get; set; }
    public required DbSet<Request> Requests { get; set; }
    public required DbSet<Approval> Approvals { get; set; }

    public UKHSA_DbContext(DbContextOptions<UKHSA_DbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder builder) => base.OnModelCreating(builder);
}
