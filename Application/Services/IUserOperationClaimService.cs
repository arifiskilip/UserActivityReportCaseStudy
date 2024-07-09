using Core.Security.Entitites;

namespace Application.Services
{
    public interface IUserOperationClaimService
    {
        Task<IList<OperationClaim>> GetUserOperationClaimsByUserId(int userId);
        Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim);
    }
}
