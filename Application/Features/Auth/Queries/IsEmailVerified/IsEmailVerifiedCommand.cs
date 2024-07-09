using Application.Features.Auth.Rules;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using MediatR;

namespace Application.Features.Auth.Queries.IsEmailVerified
{
    public class IsEmailVerifiedCommand : IRequest<IsEmailVerifiedResponse>, ISecuredRequest
    {
        public string[] Roles => [GeneralOperationClaims.User];

        public class IsEmailVerifiedHandler : IRequestHandler<IsEmailVerifiedCommand, IsEmailVerifiedResponse>
        {
            private readonly IUserService _userService;
            private readonly AuthBusinessRules _businessRules;

            public IsEmailVerifiedHandler(IUserService userService, AuthBusinessRules businessRules)
            {
                _userService = userService;
                _businessRules = businessRules;
            }

            public async Task<IsEmailVerifiedResponse> Handle(IsEmailVerifiedCommand request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAuthenticatedUserAsync();
                _businessRules.IsSelectedEntityAvailable(user);
                return new()
                {
                    IsEmailVerified = user.IsEmailVerified,
                    UserId = user.Id
                };
            }
        }
    }
}
