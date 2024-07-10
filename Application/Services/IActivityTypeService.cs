using Domain.Entities;

namespace Application.Services
{
    public interface IActivityTypeService
    {
        Task<ActivityType> GetByIdAsync(int id);
    }
}
