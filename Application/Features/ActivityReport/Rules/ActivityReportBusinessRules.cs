using Application.Features.ActivityReport.Constant;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ActivityReport.Rules
{
    public class ActivityReportBusinessRules : BaseBusinessRules
    {
        private readonly IActivityReportRepository _activityReportRepository;

        public ActivityReportBusinessRules(IActivityReportRepository activityReportRepository)
        {
            _activityReportRepository = activityReportRepository;
        }
        public async Task CheckTimeOfActivityAsync(DateTime date, int userId)
        {
            var result = await _activityReportRepository.AnyAsync(
                predicate: x => x.UserId == userId && x.Date == date);
            if (result)
            {
                throw new BusinessException(ActivityReportMessages.ActivityTimePeriodIsAvailable);
            }
        }
        public async Task SelectedEntityIsAvailableAsync(IEntity entity)
        {
            await Task.Run(() =>
            {
                if (entity is null)
                {
                    throw new BusinessException(ActivityReportMessages.UserNotFound);
                }
            });
        }
        public async Task CheckActivityTypeIsAvailableAsync(int activityTypeId)
        {
            await Task.Run(async () =>
            {
                var result = await _activityReportRepository.GetAsync(
                    predicate: x => x.ActivityType.Id == activityTypeId,
                    include: x => x.Include(i => i.ActivityType),
                    enableTracking: false);
                if (result is null)
                {
                    throw new BusinessException(ActivityReportMessages.ActivityTypeNotFound);
                }
            });
        }
        public async Task UpdateDuplicatDateCheckAsync(DateTime date, int id)
        {
            var check = await _activityReportRepository
            .GetAsync(x => x.Date == date);
            if (check != null && check.Id != id)
            {
                throw new BusinessException(ActivityReportMessages.ActivityTimePeriodIsAvailable);
            }
        }
    }
}
