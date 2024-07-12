using Application.Features.ActivityReport.Queries.GetAllPaginatedByUserId;
using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.ActivityReport.Queries.GetAllByUserId
{
    public class GetAllActivityReportsByUserIdQuery : IRequest<List<GetAllActivityReportsByUserIdResponse>>, ISecuredRequest
    {
        // Queries
        public int? ActivityTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];



        public class GetAllActivityReportsByUserIdQueryHandler : IRequestHandler<GetAllActivityReportsByUserIdQuery, List<GetAllActivityReportsByUserIdResponse>>
        {
            private readonly IUserService _userService;
            private readonly ActivityReportBusinessRules _businessRules;
            private readonly IActivityReportRepository _activityReportRepository;
            private readonly IMapper _mapper;

            public GetAllActivityReportsByUserIdQueryHandler(IUserService userService, ActivityReportBusinessRules businessRules, IActivityReportRepository activityReportRepository, IMapper mapper)
            {
                _userService = userService;
                _businessRules = businessRules;
                _activityReportRepository = activityReportRepository;
                _mapper = mapper;
            }

            public async Task<List<GetAllActivityReportsByUserIdResponse>> Handle(GetAllActivityReportsByUserIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAuthenticatedUserAsync();
                await _businessRules.SelectedEntityIsAvailableAsync(entity: user);
                var result = await _activityReportRepository.GetFilteredUserActivityReportAsync(
                    userId: user.Id,
                    activityTypeId: request.ActivityTypeId,
                    startDate: request.StartDate,
                    endDate: request.EndDate);

                List<GetAllActivityReportsByUserIdResponse> activityReports = _mapper.Map<List<GetAllActivityReportsByUserIdResponse>>(result);
                return activityReports;
            }
        }
    }
}
