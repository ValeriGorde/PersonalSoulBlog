using AutoMapper;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Account;
using PersonalSoulBlog.ViewModels.Roles;
using PersonalSoulBlog.ViewModels.User;

namespace PersonalSoulBlog.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<CreateRoleViewModel, Role>()
                .ForMember(x => x.NormalizedName, opt => opt.MapFrom(c => c.Name.ToUpperInvariant()));
            CreateMap<Role, CreateRoleViewModel>();
            CreateMap<Role, EditRoleViewModel>().ReverseMap();

            CreateMap<CreateUserViewModel, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<User, CreateUserViewModel>();
            CreateMap<User, EditUserViewModel>().ReverseMap();
        }
    }
}
