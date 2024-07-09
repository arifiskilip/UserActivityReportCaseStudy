using Core.Persistence.Repositories;
using Core.Security.Entitites;

namespace Application.Repositories
{
    public interface IUserOperationClaimRepository : IAsyncRepository<UserOperationClaim,int>, IRepository<UserOperationClaim, int>
    {
        
    }
}
