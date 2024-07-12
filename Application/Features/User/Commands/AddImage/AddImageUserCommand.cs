using Application.Repositories;
using Core.Application.Pipelines.Authorization;
using Core.Security.Constants;
using Core.Utilities.FileHelper;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features
{
    public class AddImageUserCommand : IRequest<AddImageUserResponse>, ISecuredRequest
    {
        public int UserId { get; set; }
        public IFormFile File { get; set; }

        public string[] Roles => [GeneralOperationClaims.User];


        public class AddImageUserCommandHandler : IRequestHandler<AddImageUserCommand, AddImageUserResponse>
        {
            private readonly IUserRepository _userRepository;
            private readonly UserBusinessRules _businessRules;

            public AddImageUserCommandHandler(IUserRepository userRepository, UserBusinessRules businessRules)
            {
                _userRepository = userRepository;
                _businessRules = businessRules;
            }

            public async Task<AddImageUserResponse> Handle(AddImageUserCommand request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetAsync(
                    predicate: x => x.Id == request.UserId,
                    enableTracking: true);
                await _businessRules.IsSelectedEntityAvailableAsync(user:user);
                var newImageUrl = await FileHelper.AddAsync(file: request.File, FileTypeEnum.Image);
                if(!string.IsNullOrEmpty(user.ImageUrl))
                {
                    FileHelper.Delete(user.ImageUrl);
                }
                user.ImageUrl = newImageUrl;
                await _userRepository.UpdateAsync(user);

                return new()
                {
                    Message = "Ekleme işlemi başarılı!"
                };
            }
        }
    }
}
