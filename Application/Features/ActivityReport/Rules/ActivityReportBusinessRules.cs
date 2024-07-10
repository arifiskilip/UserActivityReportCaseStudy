using Application.Features.ActivityReport.Constant;
using Application.Repositories;
using Application.Services;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ActivityReport.Rules
{
    public class ActivityReportBusinessRules : BaseBusinessRules
    {
        private readonly IActivityReportRepository _activityReportRepository;
        private readonly IActivityTypeService _activityTypeService;

        public ActivityReportBusinessRules(IActivityReportRepository activityReportRepository, IActivityTypeService activityTypeService)
        {
            _activityReportRepository = activityReportRepository;
            _activityTypeService = activityTypeService;
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
                    throw new BusinessException(ActivityReportMessages.ActivityTypeNotFound);
                }
            });
        }
        public async Task CheckActivityTypeIsAvailableAsync(int activityTypeId)
        {
            var entity = await _activityTypeService.GetByIdAsync(id: activityTypeId);
            if (entity is null)
            {
                throw new BusinessException(ActivityReportMessages.ActivityTypeNotFound);
            }
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
