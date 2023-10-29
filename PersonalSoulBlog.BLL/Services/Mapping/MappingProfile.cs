using AutoMapper;
using PersonalSoulBlog.BLL.ViewModels.Account;
using PersonalSoulBlog.BLL.ViewModels.Articles;
using PersonalSoulBlog.BLL.ViewModels.Comments;
using PersonalSoulBlog.BLL.ViewModels.Roles;
using PersonalSoulBlog.BLL.ViewModels.Tags;
using PersonalSoulBlog.BLL.ViewModels.Users;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterRequest, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<CreateUserRequest, User>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(c => c.Email));
            CreateMap<User, CreateUserRequest>();
            CreateMap<User, EditUserRequest>().ReverseMap();
            CreateMap<User, UserView>().ReverseMap();

            CreateMap<CreateRoleRequest, Role>()
                .ForMember(x => x.NormalizedName, opt => opt.MapFrom(c => c.Name.ToUpperInvariant()));
            CreateMap<Role, CreateRoleRequest>().ReverseMap();
            CreateMap<Role, EditRoleRequest>().ReverseMap();            

            CreateMap<Tag, CreateTagRequest>().ReverseMap();
            CreateMap<Tag, TagView>().ReverseMap();
            CreateMap<Tag, EditTagRequest>().ReverseMap();
            CreateMap<Tag, TagForArticleRequest>()
                .ForMember(x => x.TagId, opt => opt.MapFrom(c => c.Id))
                .ForMember(x => x.TagName, opt => opt.MapFrom(c => c.Name)).ReverseMap();

            CreateMap<CreateArticelRequest, Article>().ReverseMap();
            CreateMap<Article, EditArticleRequest>().ReverseMap(); 
            CreateMap<Article, ArticleView>()
                .ForMember(x => x.Author, opt => opt.MapFrom(c => c.User)).ReverseMap();

            CreateMap<Comment, CommentView>().ReverseMap();
            CreateMap<Comment, EditCommentRequest>().ReverseMap();
            CreateMap<Comment, CreateCommentRequest>().ReverseMap();
        }
    }
}
