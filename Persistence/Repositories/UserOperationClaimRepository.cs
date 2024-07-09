using Core.Persistence.Repositories;
using Core.Security.Entitites;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserOperationClaimRepository : EfRepositoryBase<UserOperationClaim, int, ActivityReportContext>
    {
        public UserOperationClaimRepository(ActivityReportContext context) : base(context)
        {
        }
    }
}
