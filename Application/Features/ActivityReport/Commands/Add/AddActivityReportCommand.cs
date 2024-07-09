using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.ActivityReport.Commands.Add
{
    public class AddActivityReportCommand : IRequest<AddActivityReportResponse>, ISecuredRequest
    {
        public int ActivityTypeId { get; set; }
        public string? Description { get; set; }
        public DateTime Date { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];

        public class AddActivityReportCommandHandler : IRequestHandler<AddActivityReportCommand, AddActivityReportResponse>
        {
            private readonly IActivityReportRepository _activityReportRepository;
            private readonly IMapper _mapper;
            private readonly IUserService _userService;
            private readonly ActivityReportBusinessRules _businessRules;

            public AddActivityReportCommandHandler(IActivityReportRepository activityReportRepository, IMapper mapper, IUserService userService, ActivityReportBusinessRules businessRules)
            {
                _activityReportRepository = activityReportRepository;
                _mapper = mapper;
                _userService = userService;
                _businessRules = businessRules;
            }

            public async Task<AddActivityReportResponse> Handle(AddActivityReportCommand request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAuthenticatedUserAsync();

                await _businessRules.SelectedEntityIsAvailableAsync(entity:user);
                await _businessRules.CheckTimeOfActivityAsync(date: request.Date,
                    userId: user.Id);
                await _businessRules.CheckActivityTypeIsAvailableAsync(activityTypeId: request.ActivityTypeId);
                var activityReport = _mapper.Map<Domain.Entities.ActivityReport>(request);
                activityReport.UserId = user.Id;
                await _activityReportRepository.AddAsync(entity:activityReport);
                return _mapper.Map<AddActivityReportResponse>(activityReport);
            }
        }
    }
}
