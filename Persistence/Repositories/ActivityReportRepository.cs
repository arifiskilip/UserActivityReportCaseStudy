using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ActivityReportRepository : EfRepositoryBase<ActivityReport, int, ActivityReportContext>
    {
        public ActivityReportRepository(ActivityReportContext context) : base(context)
        {
        }
    }
}
