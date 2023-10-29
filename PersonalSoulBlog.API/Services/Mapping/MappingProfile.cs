using AutoMapper;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Account;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Comments;
using PersonalSoulBlog.ViewModels.Roles;
using PersonalSoulBlog.ViewModels.Tags;
using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<CreateRoleRequest, Role>()
                .ForMember(x => x.NormalizedName, opt => opt.MapFrom(c => c.Name.ToUpperInvariant()));
            CreateMap<Role, CreateRoleRequest>();
            CreateMap<Role, EditRoleRequest>().ReverseMap();

            CreateMap<CreateUserRequest, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<User, CreateUserRequest>();
            CreateMap<User, EditUserRequest>().ReverseMap();
            CreateMap<User, UserResponse>().ReverseMap();

            CreateMap<Tag, CreateTagRequest>().ReverseMap();
            CreateMap<Tag, EditTagRequest>().ReverseMap();

            CreateMap<CreateArticelRequest, Article>().ReverseMap();
            CreateMap<Article, EditArticleRequest>().ReverseMap();

            CreateMap<Tag, TagForArticleRequest>()
                .ForMember(x => x.TagId, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.TagName, opt => opt.MapFrom(c => c.Name)).ReverseMap();

            CreateMap<Article, ArticleResponse>()
                .ForMember(x => x.Author, opt => opt.MapFrom(c => c.User)).ReverseMap();

            CreateMap<Comment, CommentResponse>().ReverseMap();
            CreateMap<Comment, EditCommentRequest>().ReverseMap();
            CreateMap<Comment, CreateCommentRequest>().ReverseMap();
        }
    }
}
