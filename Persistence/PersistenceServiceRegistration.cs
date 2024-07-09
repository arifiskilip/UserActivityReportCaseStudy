using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;

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
            return services;
        }
    }
}
