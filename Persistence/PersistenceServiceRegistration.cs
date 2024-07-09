using Application.Repositories;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories;
using Persistence.Services;

namespace Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<ActivityReportContext>(opt =>
            {
                opt.UseInMemoryDatabase("TestDb");
            });

            services.AddScoped<IActivityReportRepository, ActivityReportRepository>();
            services.AddScoped<IUserOperationClaimRepository, UserOperationClaimRepository>();
            services.AddScoped<IUserOperationClaimService, UserOperationClaimService>();
            services.AddScoped<IUserRepository, UserReporsitory>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
            services.AddScoped<IAuthService, AuthService>();
            return services;
        }
    }
}
