using Application.Features.ActivityType.Queries.GetAll;
using AutoMapper;

namespace Application.Features.ActivityType.Porifles
{
    public class ActivityTypeProfile : Profile
    {
        public ActivityTypeProfile()
        {
            CreateMap<Domain.Entities.ActivityType, GetAllActivityTypeResponse>().ReverseMap();
        }
    }
}
