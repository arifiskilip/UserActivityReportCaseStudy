using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Helpers;
using Application.Repositories;
using Application.Services;
using Core.Mailing;
using Core.Mailing.Constant;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Queries.PasswordResetSendEmail
{
    public class PasswordResetSendEmailQuery : IRequest<PasswordResetSendEmailResponse>
    {
        public string Email { get; set; }


        public class PasswordResetSendEmailHandler : IRequestHandler<PasswordResetSendEmailQuery, PasswordResetSendEmailResponse>
        {
            private readonly IUserService _userService;
            private readonly IVerificationCodeRepository _verificationCodeRepository;
            private readonly AuthBusinessRules _businessRules;
            private readonly IEmailService _emailService;

            public PasswordResetSendEmailHandler(IUserService userService, IVerificationCodeRepository verificationCodeRepository, AuthBusinessRules businessRules, IEmailService emailService)
            {
                _userService = userService;
                _verificationCodeRepository = verificationCodeRepository;
                _businessRules = businessRules;
                _emailService = emailService;
            }

            public async Task<PasswordResetSendEmailResponse> Handle(PasswordResetSendEmailQuery request, CancellationToken cancellationToken)
            {
                var checkUser = await _userService.GetAuthenticatedUserAsync();
                await _businessRules.IsSelectedEntityAvailableAsync(checkUser);

                var existingVerificationCode = await _verificationCodeRepository.GetAsync(
                    predicate: x => x.UserId == checkUser.Id && x.CodeTypeId == (int)CodeTypeEnum.PasswordReset
                );

                string code = VerificationCodeHelper.GenerateVerificationCode();
                DateTime expirationDate = DateTime.Now.AddMinutes(3);

                if (existingVerificationCode is null)
                {
                    var newVerificationCode = new VerificationCode
                    {
                        CodeTypeId = (int)CodeTypeEnum.PasswordReset,
                        UserId = checkUser.Id,
                        ExpirationDate = expirationDate,
                        Code = code
                    };

                    await _verificationCodeRepository.AddAsync(newVerificationCode);
                }
                else
                {
                    existingVerificationCode.Code = code;
                    existingVerificationCode.ExpirationDate = expirationDate;

                    await _verificationCodeRepository.UpdateAsync(existingVerificationCode);
                }

                await _emailService.SendEmailAsync(request.Email, AuthMessages.PasswordReset, HtmlBody.PasswordReset(code));

                return new()
                {
                    Email = request.Email
                };
            }
        }

    }
}
