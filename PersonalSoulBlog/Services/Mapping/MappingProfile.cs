using AutoMapper;
using PersonalSoulBlog.Models.Entities;
using PersonalSoulBlog.ViewModels;

namespace PersonalSoulBlog.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
        }
    }
}
