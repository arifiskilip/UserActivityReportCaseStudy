using Application.Features.Auth.Commands.Login;
using Application.Features.Auth.Commands.Register;
using Application.Features.Auth.Queries.SendVerificationCode;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Auth.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterCommand, User>().ReverseMap();
            CreateMap<LoginResponse, User>().ReverseMap();
            CreateMap<VerificationCode, SendVerificationCodeResponse>().ReverseMap();
        }
    }
}
