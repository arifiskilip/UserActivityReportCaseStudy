using Application.Features.Auth.Constant;
using Application.Features.Auth.Rules;
using Application.Helpers;
using Application.Repositories;
using Application.Services;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Mailing;
using Core.Mailing.Constant;
using Core.Security.Constants;
using Domain.Entities;
using Domain.Enums;
using MediatR;

namespace Application.Features.Auth.Queries.SendVerificationCode
{
    public class SendVerificationCodeCommand : IRequest<SendVerificationCodeResponse>, ISecuredRequest
    {
        public string[] Roles => [GeneralOperationClaims.User];

        public class VerificationCodeHandler : IRequestHandler<SendVerificationCodeCommand, SendVerificationCodeResponse>
        {
            private readonly IUserService _userService;
            private readonly IMapper _mapper;
            private readonly IEmailService _emailService;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IVerificationCodeRepository _verificationCodeRepository;

            public VerificationCodeHandler(IUserService userService, IMapper mapper, IEmailService emailService, AuthBusinessRules authBusinessRules, IVerificationCodeRepository verificationCodeRepository)
            {
                _userService = userService;
                _mapper = mapper;
                _emailService = emailService;
                _authBusinessRules = authBusinessRules;
                _verificationCodeRepository = verificationCodeRepository;
            }

            public async Task<SendVerificationCodeResponse> Handle(SendVerificationCodeCommand request, CancellationToken cancellationToken)
            {
                var user = await _userService.GetAuthenticatedUserAsync();
                int codeTypeId = (int)CodeTypeEnum.EmailConfirm;

                await _authBusinessRules.CheckUserByIdAsync(userId: user.Id);

                string? userEmail = user.Email;
                var existingVerificationCode = await _verificationCodeRepository.GetAsync(
                    predicate: x => x.UserId == user.Id && x.CodeTypeId == codeTypeId);

                var verificationCode = existingVerificationCode ?? await CreateNewVerificationCode(user.Id, codeTypeId);
                verificationCode.Code = VerificationCodeHelper.GenerateVerificationCode();
                verificationCode.ExpirationDate = DateTime.Now.AddMinutes(1);

                await _verificationCodeRepository.UpdateAsync(verificationCode);
                await _emailService.SendEmailAsync(userEmail, AuthMessages.VerificationCode, HtmlBody.OtpVerified(verificationCode.Code));

                return _mapper.Map<SendVerificationCodeResponse>(verificationCode);
            }
            private async Task<VerificationCode> CreateNewVerificationCode(int userId, int codeTypeId)
            {
                var verificationCode = new VerificationCode
                {
                    CodeTypeId = codeTypeId,
                    UserId = userId,
                    ExpirationDate = DateTime.Now.AddMinutes(1),
                    Code = VerificationCodeHelper.GenerateVerificationCode()
                };
                return await _verificationCodeRepository.AddAsync(verificationCode);
            }
        }
    }
}
