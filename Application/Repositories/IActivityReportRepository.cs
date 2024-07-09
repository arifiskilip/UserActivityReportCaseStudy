using Core.Persistence.Paging;
using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IActivityReportRepository : IAsyncRepository<ActivityReport, int>, IRepository<ActivityReport, int>
    {
        Task<IPaginate<ActivityReport>> GetPaginatedFilteredUserActivityReportAsync(int userId, int? activityTypeId, DateTime? startDate, DateTime? endDate, int pageSize, int pageIndex);
    }
}
