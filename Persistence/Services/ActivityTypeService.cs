using Application.Features.ActivityReport.Constant;
using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;

namespace Persistence.Services
{
    public class ActivityTypeService : IActivityTypeService
    {
        private readonly IActivityTypeRepository _activityTypeRepository;

        public ActivityTypeService(IActivityTypeRepository activityTypeRepository)
        {
            _activityTypeRepository = activityTypeRepository;
        }

        public async Task<ActivityType> GetByIdAsync(int id)
        {
            var checkEntity = await _activityTypeRepository.GetAsync(
                predicate: x => x.Id == id);
            if (checkEntity == null)
            {
                throw new BusinessException(ActivityReportMessages.ActivityTypeNotFound);
            }
            return checkEntity;
        }
    }
}
