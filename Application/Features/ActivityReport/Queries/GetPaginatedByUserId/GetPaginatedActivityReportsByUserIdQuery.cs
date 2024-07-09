using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.ActivityReport.Queries.GetAllPaginatedByUserId
{
    public class GetPaginatedActivityReportsByUserIdQuery : IRequest<IPaginate<GetPaginatedActivityReportsByUserIdResponse>>, ISecuredRequest
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Queries
        public int? ActivityTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];


        public class ActivityReportGetAllPaginatedByUserIdQueryHandler : IRequestHandler<GetPaginatedActivityReportsByUserIdQuery, IPaginate<GetPaginatedActivityReportsByUserIdResponse>>
        {
            private readonly IUserService _userService;
            private readonly ActivityReportBusinessRules _businessRules;
            private readonly IActivityReportRepository _activityReportRepository;
            private readonly IMapper _mapper;

            public ActivityReportGetAllPaginatedByUserIdQueryHandler(IActivityReportRepository activityReportRepository, IMapper mapper, IUserService userService, ActivityReportBusinessRules businessRules)
            {
                _activityReportRepository = activityReportRepository;
                _mapper = mapper;
                _userService = userService;
                _businessRules = businessRules;
            }

            public async Task<IPaginate<GetPaginatedActivityReportsByUserIdResponse>> Handle(GetPaginatedActivityReportsByUserIdQuery request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAuthenticatedUserAsync();
                await _businessRules.SelectedEntityIsAvailableAsync(entity: user);
                var result = await _activityReportRepository.GetPaginatedFilteredUserActivityReportAsync(
                    pageIndex: request.PageIndex,
                    pageSize: request.PageSize,
                    userId:user.Id,
                    activityTypeId:request.ActivityTypeId,
                    startDate:request.StartDate,
                    endDate:request.EndDate);

                List<GetPaginatedActivityReportsByUserIdResponse> activityReports = _mapper.Map<List<GetPaginatedActivityReportsByUserIdResponse>>(result.Items);

                return new Paginate<GetPaginatedActivityReportsByUserIdResponse>(activityReports.AsQueryable(), result.Pagination);
            }
        }
    }
}
