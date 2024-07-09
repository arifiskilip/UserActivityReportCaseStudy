using Application.Services;
using Core.Security.JWT;
using Domain.Entities;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserOperationClaimService _userOperationClaimService;

        public AuthService(ITokenHelper tokenHelper, IUserOperationClaimService userOperationClaimService)
        {
            _tokenHelper = tokenHelper;
            _userOperationClaimService = userOperationClaimService;
        }

        public async Task<AccessToken> CreateAccessToken(User user)
        {
            var operationClaims = await _userOperationClaimService.GetUserOperationClaimsByUserId(
                userId: user.Id);
            AccessToken token = _tokenHelper.CreateToken(
                user: user, operationClaims: operationClaims);
            return token;
        }
    }
}
