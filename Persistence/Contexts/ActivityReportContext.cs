using Core.Security.Entitites;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts
{
    public class ActivityReportContext : DbContext
    {
        public ActivityReportContext(DbContextOptions<ActivityReportContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ActivityReport> ActivityReports { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
