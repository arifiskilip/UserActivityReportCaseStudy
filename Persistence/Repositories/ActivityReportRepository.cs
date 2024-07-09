using Application.Repositories;
using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class ActivityReportRepository : EfRepositoryBase<ActivityReport, int, ActivityReportContext>, IActivityReportRepository
    {
        public ActivityReportRepository(ActivityReportContext context) : base(context)
        {
        }

        public async Task<IPaginate<ActivityReport>> GetPaginatedFilteredUserActivityReportAsync(int userId, int? activityTypeId, DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex)
        {
            IQueryable<ActivityReport> query = Context.ActivityReports
                 .AsQueryable()
                 .AsNoTracking()
                 .Include(i => i.ActivityType)
                 .Where(x => x.UserId == userId);

            if (activityTypeId.HasValue)
            {
                query = query.Where(ai => ai.ActivityTypeId == activityTypeId.Value);
            }

            if (startDate.HasValue)
            {
                query = query.Where(ai => ai.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(ai => ai.Date <= endDate.Value.Date.AddDays(1).AddTicks(-1));
            }

            if (!startDate.HasValue && !endDate.HasValue)
            {
                query = query.Where(ai => ai.Date >= DateTime.UtcNow);
            }

            return Paginate<ActivityReport>.Create(
                source: query,
                pageIndex: pageIndex,
                pageSize: pageSize
            );
        }
    }
}
