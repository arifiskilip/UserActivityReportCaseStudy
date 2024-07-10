using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Helpers;
using Application.Repositories;
using Application.Services;
using Core.Mailing;
using Core.Mailing.Constant;
using Core.Security.Hashing;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Commands.PasswordResetCodeVerified
{
    public class PasswordResetCodeVerifiedCommand : IRequest<PasswordResetCodeVerifiedResponse>
    {
        public string Code { get; set; }
        public string Email { get; set; }

        public class PasswordResetCodeVerifiedHandler : IRequestHandler<PasswordResetCodeVerifiedCommand, PasswordResetCodeVerifiedResponse>
        {
            private readonly IEmailService _emailService;
            private readonly IUserService _userService;
            private readonly IVerificationCodeRepository _verificationCodeRepository;
            private readonly AuthBusinessRules _rules;

            public PasswordResetCodeVerifiedHandler(IEmailService emailService, IUserService userService, IVerificationCodeRepository verificationCodeRepository, AuthBusinessRules rules)
            {
                _emailService = emailService;
                _userService = userService;
                _verificationCodeRepository = verificationCodeRepository;
                _rules = rules;
            }

            public async Task<PasswordResetCodeVerifiedResponse> Handle(PasswordResetCodeVerifiedCommand request, CancellationToken cancellationToken)
            {
                var checkUser = await _userService.GetUserByEmailAsync(request.Email);
                await _rules.IsSelectedEntityAvailableAsync(checkUser);

                var verificationCode = await _verificationCodeRepository.GetAsync(x => x.UserId == checkUser.Id && x.CodeTypeId == (int)CodeTypeEnum.PasswordReset);
                await _rules.CheckVerificationCodeTimeAsync(verificationCode: verificationCode);
                await _rules.CheckVerificationCodeAsync(userVerificationCode: verificationCode.Code,
                    verificationCode: request.Code);

                byte[] passwordHash, passwordSalt;
                string newPassword = PasswordHelper.RandomPasswordGenerator();
                HashingHelper.CreatePasswordHash(
                    password: newPassword,
                    passwordHash: out passwordHash,
                    passwordSalt: out passwordSalt);
                checkUser.PasswordHash = passwordHash;
                checkUser.PasswordSalt = passwordSalt;

                await _userService.UpdateAsync(checkUser);

                await _emailService.SendEmailAsync(
                    toEmail: request.Email,
                    subject: AuthMessages.PasswordReset,
                    body: HtmlBody.NewPassword(newPassword));

                return new()
                {
                    Message = AuthMessages.SuccessVerificationCode
                };
            }
        }
    }
}
