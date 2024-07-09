using Application.Repositories;
using Application.Services;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Core.Security.Extensions;
using System.Security.Claims;
using Application.Features.Auth.Constant;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IUserRepository userRepository, IHttpContextAccessor httpContext)
        {
            _userRepository = userRepository;
            _httpContext = httpContext;
        }

        public async Task<User> GetAuthenticatedUserAsync()
        {
            string? id = _httpContext?.HttpContext?.User?.Claims(ClaimTypes.NameIdentifier)?.FirstOrDefault();
            if (id is null) throw new BusinessException(AuthMessages.UserNotFound);
            return await _userRepository.GetAsync(x => x.Id == int.Parse(id));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var checkUser = await _userRepository.GetAsync(x => x.Email.ToLower() == email.ToLower());
            if (checkUser is null)
            {
                throw new BusinessException(AuthMessages.UserNotFound);
            }
            return checkUser;
        }

        public async Task SetUserEmailVerified(int userId)
        {
            var checkUser = await _userRepository.GetAsync(x => x.Id == userId);
            if (checkUser is null)
            {
                throw new BusinessException(AuthMessages.UserNotFound);
            }
            checkUser.IsEmailVerified = true;
            await _userRepository.UpdateAsync(checkUser);

        }

        public async Task<User> UpdateAsync(User user)
        {
            var result = await _userRepository.UpdateAsync(user);
            return result;
        }
    }
}
