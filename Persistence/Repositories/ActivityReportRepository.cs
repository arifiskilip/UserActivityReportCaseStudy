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

        public async Task<IQueryable<ActivityReport>> GetFilteredUserActivityReportAsync(int userId, int? activityTypeId, DateTime? startDate, DateTime? endDate)
        {
            IQueryable<ActivityReport> query = Context.ActivityReports
               .AsQueryable()
               .AsNoTracking()
               .OrderByDescending(o => o.Date)
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

            return query;
        }

        public async Task<IPaginate<ActivityReport>> GetPaginatedFilteredUserActivityReportAsync(int userId, int? activityTypeId, DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex)
        {
            IQueryable<ActivityReport> query = Context.ActivityReports
                 .AsQueryable()
                 .AsNoTracking()
                 .OrderByDescending(o=> o.Date)
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

            return Paginate<ActivityReport>.Create(
                source: query,
                pageIndex: pageIndex,
                pageSize: pageSize
            );
        }
    }
}
