using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Persistence.Paging;
using Core.Security.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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
                Expression<Func<Domain.Entities.ActivityReport, bool>> prediacte = x => x.UserId == user.Id;
                var result = await _activityReportRepository.GetListAsync(
                    predicate: x => x.UserId == user.Id,
                    include: x => x.Include(i => i.ActivityType),
                    index: request.PageIndex,
                    size: request.PageSize,
                    enableTracking: false);
                List<GetPaginatedActivityReportsByUserIdResponse> activityReports = _mapper.Map<List<GetPaginatedActivityReportsByUserIdResponse>>(result.Items);

                return new Paginate<GetPaginatedActivityReportsByUserIdResponse>(activityReports.AsQueryable(), result.Pagination);
            }
        }
    }
}
