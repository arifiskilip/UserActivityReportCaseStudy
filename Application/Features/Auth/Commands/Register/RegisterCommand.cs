using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Transaction;
using Core.Security.Constants;
using Core.Security.Hashing;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>, ITransactionalRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly IUserOperationClaimService _userOperationClaimService;
            private readonly IAuthService _authService;
            private readonly AuthBusinessRules _businessRules;
            private readonly IMapper _mapper;

            public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, AuthBusinessRules businessRules, IMapper mapper, IUserOperationClaimService userOperationClaimService)
            {
                _userRepository = userRepository;
                _authService = authService;
                _businessRules = businessRules;
                _mapper = mapper;
                _userOperationClaimService = userOperationClaimService;
            }

            public async Task<RegisterResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
            {
                User? checkUser = await _userRepository.GetAsync(
                    predicate: x => x.Email.ToLower() == request.Email.ToLower(),
                    enableTracking: false,
                    cancellationToken: cancellationToken);

                _businessRules.IsSelectedEntityAvailable(checkUser);

                byte[] passwordHash, passwordSalt;

                HashingHelper.CreatePasswordHash(
                    password: request.Password,
                    passwordHash: out passwordHash,
                    passwordSalt: out passwordSalt);

                User user = _mapper.Map<User>(request);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                await _userRepository.AddAsync(user);
                await _userOperationClaimService.AddAsync(new()
                {
                    UserId = user.Id,
                    OperationClaimId = (int)OperationClaimEnum.User
                });

                AccessToken? token = await _authService.CreateAccessToken(
                    user: user);
                return new()
                {
                    AccessToken = token
                };
            }
        }
    }
}
