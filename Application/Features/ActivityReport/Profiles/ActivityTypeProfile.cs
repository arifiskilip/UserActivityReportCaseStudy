using Application.Features.ActivityReport.Commands.Add;
using Application.Features.ActivityReport.Commands.Update;
using Application.Features.ActivityReport.Queries.GetAllPaginatedByUserId;
using Application.Features.ActivityReport.Queries.GetById;
using AutoMapper;

namespace Application.Features.ActivityReport.Profiles
{
    public class ActivityTypeProfile : Profile
    {
        public ActivityTypeProfile()
        {
            CreateMap<Domain.Entities.ActivityReport, AddActivityReportCommand>().ReverseMap();
            CreateMap<Domain.Entities.ActivityReport, AddActivityReportResponse>()
                .ForMember(src => src.ActivityTypeName, opt => opt.MapFrom(x => x.ActivityType.Name));

            CreateMap<UpdateActivityReportCommand, Domain.Entities.ActivityReport>()
                 .ForMember(dest => dest.User, opt => opt.Ignore())
                 .ForMember(dest => dest.ActivityType, opt => opt.Ignore());
            CreateMap<Domain.Entities.ActivityReport, UpdateActivityReportResponse>()
                .ForMember(src => src.ActivityTypeName, opt => opt.MapFrom(x => x.ActivityType.Name));
            CreateMap<Domain.Entities.ActivityReport, GetPaginatedActivityReportsByUserIdResponse>()
                .ForMember(src => src.ActivityTypeName, opt => opt.MapFrom(x => x.ActivityType.Name));
            CreateMap<Domain.Entities.ActivityReport, GetByIdActivityReportResponse>()
               .ForMember(src => src.ActivityTypeName, opt => opt.MapFrom(x => x.ActivityType.Name));
        }
    }
}
