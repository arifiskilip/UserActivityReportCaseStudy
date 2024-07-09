using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IActivityReportRepository : IAsyncRepository<ActivityReport, int>, IRepository<ActivityReport, int>
    {
    }
}
