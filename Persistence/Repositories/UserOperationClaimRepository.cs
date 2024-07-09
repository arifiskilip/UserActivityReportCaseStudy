using Application.Repositories;
using Core.Persistence.Repositories;
using Core.Security.Entitites;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int, ActivityReportContext>, IUserOperationClaimRepository
    {
        public UserOperationClaimRepository(ActivityReportContext context) : base(context)
        {
        }
    }
}
