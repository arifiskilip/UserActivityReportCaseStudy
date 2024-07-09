using Core.Persistence.Repositories;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRepository: IRepository<User, int>, IAsyncRepository<User, int>
    {
    }
}
