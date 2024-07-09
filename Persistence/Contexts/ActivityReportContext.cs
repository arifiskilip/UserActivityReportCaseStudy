using Core.Domain;
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
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<Entity<int>>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Modified)
                {
                    entity.Entity.UpdatedDate = DateTime.UtcNow;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<CodeType> CodeTypes { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<ActivityReport> ActivityReports { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    }
}
