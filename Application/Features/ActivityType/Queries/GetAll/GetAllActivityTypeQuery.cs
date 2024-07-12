using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.ActivityType.Queries.GetAll
{
    public class GetAllActivityTypeQuery : IRequest<List<GetAllActivityTypeResponse>>, ISecuredRequest
    {
        public string[] Roles => [GeneralOperationClaims.User];


        public class GetAllActivityTypeQueryHandler : IRequestHandler<GetAllActivityTypeQuery, List<GetAllActivityTypeResponse>>
        {
            private readonly IActivityTypeRepository _activityTypeRepository;
            private readonly IMapper _mapper;

            public GetAllActivityTypeQueryHandler(IActivityTypeRepository activityTypeRepository, IMapper mapper)
            {
                _activityTypeRepository = activityTypeRepository;
                _mapper = mapper;
            }

            public async Task<List<GetAllActivityTypeResponse>> Handle(GetAllActivityTypeQuery request, CancellationToken cancellationToken)
            {
                var result = await _activityTypeRepository.GetListNotPagedAsync(
                    enableTracking: false,
                    orderBy: o => o.OrderBy(x => x.Name));

                return _mapper.Map<List<GetAllActivityTypeResponse>>(result);
            }
        }
    }
}
