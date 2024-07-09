using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class UserReporsitory : EfRepositoryBase<User, int, ActivityReportContext>, IUserRepository
    {
        public UserReporsitory(ActivityReportContext context) : base(context)
        {
        }
    }
}
