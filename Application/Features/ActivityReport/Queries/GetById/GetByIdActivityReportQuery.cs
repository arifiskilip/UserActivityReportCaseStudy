using Application.Features.ActivityReport.Rules;
using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.ActivityReport.Queries.GetById
{
    public class GetByIdActivityReportQuery : IRequest<GetByIdActivityReportResponse>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];


        public class GetByIdActivityReportQueryHandler : IRequestHandler<GetByIdActivityReportQuery, GetByIdActivityReportResponse>
        {
            private readonly ActivityReportBusinessRules _businessRules;
            private readonly IActivityReportRepository _activityReportRepository;
            private readonly IMapper _mapper;

            public GetByIdActivityReportQueryHandler(ActivityReportBusinessRules businessRules, IActivityReportRepository activityReportRepository, IMapper mapper)
            {
                _businessRules = businessRules;
                _activityReportRepository = activityReportRepository;
                _mapper = mapper;
            }

            public async Task<GetByIdActivityReportResponse> Handle(GetByIdActivityReportQuery request, CancellationToken cancellationToken)
            {
                var activityReport = await _activityReportRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: false,
                    include: x => x.Include(i => i.ActivityType));
                await _businessRules.SelectedEntityIsAvailableAsync(entity: activityReport);
                return _mapper.Map<GetByIdActivityReportResponse>(activityReport);
            }
        }
    }
}
