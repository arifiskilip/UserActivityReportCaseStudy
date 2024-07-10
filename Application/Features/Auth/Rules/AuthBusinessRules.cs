using Application.Features.Auth.Constant;
using Application.Repositories;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Core.Security.Hashing;
using Domain.Entities;

namespace Application.Features.Auth.Rules
{
    public class AuthBusinessRules : BaseBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task DuplicateEmailChechAsync(string email)
        {
            bool check = await _userRepository.AnyAsync(
                predicate: x => x.Email.ToLower() == email.ToLower(),
                enableTracking: false);
            if (check) throw new BusinessException(AuthMessages.DuplicateEmail);
        }

        public async Task CheckUserByIdAsync(int userId)
        {
            bool check = await _userRepository.AnyAsync(x => x.Id == userId);
            if (!check) throw new BusinessException(AuthMessages.UserNotFound);
        }

        public async Task IsPasswordCorrectWhenLoginAsync(User user, string password)
        {
            await Task.Run(() =>
            {
                var check = HashingHelper.VerifyPasswordHash(password: password, passwordHash: user.PasswordHash, passwordSalt: user.PasswordSalt);
                if (!check) throw new BusinessException(AuthMessages.UserEmailNotFound);
            });
        }

        public async Task<User> UserEmailCheckAsync(string email)
        {
            var user = await _userRepository.GetAsync(
                predicate: x => x.Email.ToLower() == email.ToLower(),
                enableTracking:false);
            if (user is null) throw new BusinessException(AuthMessages.UserNotFound);
            return user;
        }

        public async Task CheckNewPasswordsMatchAsync(string newPassword, string newPasswordAgain)
        {
            await Task.Run(() =>
            {
                if (newPassword != newPasswordAgain)
                {
                    throw new BusinessException(AuthMessages.PasswordsDontMatch);
                }
            });
        }

        public async Task<string> GetUserEmailAsync(int userId)
        {
            var user = await _userRepository.GetAsync(
                predicate: x => x.Id == userId);
            if (user is not null)
            {
                return user.Email;
            }
            throw new BusinessException(AuthMessages.UserNotFound);
        }

        public async Task IsSelectedEntityAvailableAsync(User? user)
        {
            await Task.Run(() =>
            {
                if (user == null) throw new BusinessException(AuthMessages.UserNotFound);
            });
        }
        public async Task IsCurrentPasswordCorrectAsync(User user, string currentPassword)
        {
            await Task.Run(() =>
            {
                var check = HashingHelper.VerifyPasswordHash(currentPassword, user.PasswordHash, user.PasswordSalt);
                if (!check) throw new BusinessException(AuthMessages.CurrentPasswordWrong);
            });
        }
        public async Task CheckVerificationCodeTimeAsync(VerificationCode verificationCode)
        {
            await Task.Run(() =>
            {
                if (!(verificationCode.ExpirationDate >= DateTime.Now))
                {
                    throw new BusinessException(AuthMessages.VerificationCodeTimeout);
                }
            });
        }
        public async Task CheckVerificationCodeAsync(string userVerificationCode, string verificationCode)
        {
            await Task.Run(() =>
            {
                if (userVerificationCode != verificationCode)
                {
                    throw new BusinessException(AuthMessages.IncorrectVerificationCode);
                }
            });
        }
    }
}

