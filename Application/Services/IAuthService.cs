using Core.Security.JWT;
using Domain.Entities;

namespace Application.Services
{
    public interface IAuthService
    {
        public Task<AccessToken> CreateAccessToken(User user);
    }
}
