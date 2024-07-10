using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Core.Security.Hashing;
using MediatR;

namespace Application.Features.Auth.Commands.UpdatePassword
{
    public class UpdatePasswordCommand : IRequest<UpdatePasswordResponse>, ISecuredRequest
    {
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string[] Roles => [GeneralOperationClaims.User,GeneralOperationClaims.Admin];
        public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommand, UpdatePasswordResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IUserService _userService;

            public UpdatePasswordCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IUserService userService)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _userService = userService;
            }

            public async Task<UpdatePasswordResponse> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
            {

                // Kullanıcı ID'sini HttpContext'ten al
                var user = await _userService.GetAuthenticatedUserAsync();

                await _authBusinessRules.IsSelectedEntityAvailableAsync(user);
                await _authBusinessRules.IsCurrentPasswordCorrectAsync(user, request.OldPassword);
                await _authBusinessRules.CheckNewPasswordsMatchAsync(request.Password, request.ConfirmPassword);

                byte[] passwordHash, passwordSalt;

                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.UpdateAsync(user);

                return new UpdatePasswordResponse
                {
                    Message = AuthMessages.PasswordChangeSuccessful
                };
            }
        }
    }
}
