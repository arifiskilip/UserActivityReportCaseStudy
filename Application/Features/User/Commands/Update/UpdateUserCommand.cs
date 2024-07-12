using Application.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features
{
    public class UpdateUserCommand : IRequest<UpdateUserResponse>, ISecuredRequest
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];


        public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly UserBusinessRules _businessRules;

            public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper, UserBusinessRules businessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _businessRules = businessRules;
            }

            public async Task<UpdateUserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(
                    predicate: x => x.Id == request.Id,
                    enableTracking: true);
                await _businessRules.IsSelectedEntityAvailableAsync(user: user);
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                await _userRepository.UpdateAsync(user);
                return _mapper.Map<UpdateUserResponse>(user);
            }
        }
    }
}
