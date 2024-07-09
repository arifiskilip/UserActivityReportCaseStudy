using Application.Features.Auth.Rules;
using Application.Services;
using AutoMapper;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.JWT;
using Domain.Entities;
using MediatR;

namespace Application.Features.Auth.Commands.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }


        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IMapper _mapper;
            private readonly IAuthService _authService;

            public LoginCommandHandler(AuthBusinessRules authBusinessRules, IMapper mapper, IAuthService authService)
            {
                _authBusinessRules = authBusinessRules;
                _mapper = mapper;
                _authService = authService;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                User user = await _authBusinessRules.UserEmailCheck(request.Email);
                _authBusinessRules.IsPasswordCorrectWhenLogin(user, request.Password);

                AccessToken token = await _authService.CreateAccessToken(user: user);
                LoginResponse response = _mapper.Map<LoginResponse>(user);
                response.AccessToken = token;
                return response;
            }
        }
    }
}
