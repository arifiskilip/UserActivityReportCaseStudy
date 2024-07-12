using Application.Features.Auth.Constant;
using Core.Application.Rules;
using Core.CrossCuttingConcers.Exceptions.Types;
using Domain.Entities;

namespace Application.Features
{
    public class UserBusinessRules : BaseBusinessRules
    {
        public async Task IsSelectedEntityAvailableAsync(User? user)
        {
            await Task.Run(() =>
            {
                if (user == null) throw new BusinessException(AuthMessages.UserNotFound);
            });
        }
    }
}
