using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Repositories;
using Application.Services;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.EmailVerified
{
    public class EmailVerifiedCommand : IRequest<EmailVerifiedResponse>, ISecuredRequest
    {
        public string Code { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];

        public class EmailVerifiedHandler : IRequestHandler<EmailVerifiedCommand, EmailVerifiedResponse>
        {
            private readonly IUserService _userService;
            private readonly IVerificationCodeRepository _verificationCodeRepository;
            private readonly AuthBusinessRules _rules;

            public EmailVerifiedHandler(IVerificationCodeRepository verificationCodeRepository, AuthBusinessRules rules, IUserService userService)
            {
                _verificationCodeRepository = verificationCodeRepository;
                _rules = rules;
                _userService = userService;
            }

            public async Task<EmailVerifiedResponse> Handle(EmailVerifiedCommand request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAuthenticatedUserAsync();

                await _rules.CheckUserByIdAsync(
                    userId:user.Id);

                var verificationCode = await _verificationCodeRepository.GetAsync(
                    predicate: x => x.UserId == user.Id && x.CodeTypeId == (int)CodeTypeEnum.EmailConfirm);

                await _rules.CheckVerificationCodeTimeAsync(verificationCode);
                await _rules.CheckVerificationCodeAsync(
                    userVerificationCode: verificationCode.Code,
                    verificationCode: request.Code);

                await _userService.SetUserEmailVerified(userId: user.Id);

                return new()
                {
                    Message = AuthMessages.SuccessVerificationCode
                };
            }
        }
    }
}
