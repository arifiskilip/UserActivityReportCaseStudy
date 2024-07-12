using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features
{
    public class GetByIdUserQuery : IRequest<GetByIdUserResponse>, ISecuredRequest
    {
        public int Id { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];



        public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, GetByIdUserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _businessRules;

            public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules businessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<GetByIdUserResponse> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
            {

                var user = await _userRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: false);
                await _businessRules.IsSelectedEntityAvailableAsync(user: user);
                return _mapper.Map<GetByIdUserResponse>(user);
            }
        }
    }
}
