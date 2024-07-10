using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ActivityTypeRepository : EfRepositoryBase<ActivityType, int, ActivityReportContext>, IActivityTypeRepository
    {
        public ActivityTypeRepository(ActivityReportContext context) : base(context)
        {
        }
    }
}
