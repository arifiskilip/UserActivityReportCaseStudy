using Core.Security.Entitites;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Seeds
{
    public static class SeedData
    {
        public static void Initialize(ActivityReportContext context)
        {
            if (!context.ActivityTypes.Any())
            {
                context.ActivityTypes.AddRange(
                    new ActivityType { Id = 1, Name = "Reading", CreatedDate = DateTime.UtcNow, Status = true },
                    new ActivityType { Id = 2, Name = "Swimming", CreatedDate = DateTime.UtcNow, Status = true },
                    new ActivityType { Id = 3, Name = "Sport", CreatedDate = DateTime.UtcNow, Status = true },
                    new ActivityType { Id = 4, Name = "Cinema", CreatedDate = DateTime.UtcNow, Status = true }
                );
                context.SaveChanges();
            }

            if (!context.CodeTypes.Any())
            {
                context.CodeTypes.AddRange(
                    new CodeType { Id = 1, Name = "EmailConfirm", CreatedDate = DateTime.UtcNow, Status = true },
                    new CodeType { Id = 2, Name = "PasswordReset", CreatedDate = DateTime.UtcNow, Status = true }
                );
                context.SaveChanges();
            }

            if (!context.OperationClaims.Any())
            {
                context.OperationClaims.AddRange(
                    new OperationClaim { Id = 1, Name = "Admin", CreatedDate = DateTime.UtcNow, Status = true },
                    new OperationClaim { Id = 2, Name = "User", CreatedDate = DateTime.UtcNow, Status = true }
                );
                context.SaveChanges();
            }
        }
    }
}

