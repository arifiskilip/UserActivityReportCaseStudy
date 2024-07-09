using Application.Repositories;
using Core.Persistence.Repositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories
{
    public class VerificationCodeRepository : EfRepositoryBase<VerificationCode, int, ActivityReportContext>, IVerificationCodeRepository
    {
        public VerificationCodeRepository(ActivityReportContext context) : base(context)
        {
        }
    }
}
