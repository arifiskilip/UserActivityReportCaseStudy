using AutoMapper;
using Domain.Entities;

namespace Application.Features
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UpdateUserResponse>().ReverseMap();
            CreateMap<User, GetByIdUserResponse>().ReverseMap();
        }
    }
}
