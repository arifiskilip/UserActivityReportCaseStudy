using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IActivityTypeRepository : IAsyncRepository<ActivityType, int>, IRepository<ActivityType, int>
    {
    }
}
