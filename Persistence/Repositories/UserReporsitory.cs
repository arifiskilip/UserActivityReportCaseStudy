using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserReporsitory : EfRepositoryBase<User, int, ActivityReportContext>
    {
        public UserReporsitory(ActivityReportContext context) : base(context)
        {
        }
    }
}
