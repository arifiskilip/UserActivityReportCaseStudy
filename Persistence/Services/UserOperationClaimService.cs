using Application.Repositories;
using Application.Services;
using Core.Security.Entitites;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;

namespace Persistence.Services
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;

        public UserOperationClaimService(IUserOperationClaimRepository userOperationClaimRepository)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
        }

        public async Task<UserOperationClaim> AddAsync(UserOperationClaim userOperationClaim)
        {
            await _userOperationClaimRepository.AddAsync(userOperationClaim);
            return userOperationClaim;
        }

        public async Task<IList<OperationClaim>> GetUserOperationClaimsByUserId(int userId)
        {

         var result = await _userOperationClaimRepository.GetListNotPagedAsync(
              predicate: x => x.UserId == userId,
              enableTracking: true,
              include: i => i.Include(x => x.OperationClaim));

            var operationClaims = result.Select(x => new OperationClaim
            {
                Id = x.OperationClaim.Id,
                CreatedDate = x.OperationClaim.CreatedDate,
                Name = x.OperationClaim.Name,
                UpdatedDate = x.OperationClaim.UpdatedDate
            }).ToList();

            return await Task.FromResult(operationClaims);
        }
    }
}
