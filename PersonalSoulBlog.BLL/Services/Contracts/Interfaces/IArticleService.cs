using PersonalSoulBlog.BLL.ViewModels.Articles;
using PersonalSoulBlog.BLL.ViewModels.Comments;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.Services.Contracts.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticles();
        Task<ArticleView> GetArticleById(Guid id);
        Task Create(CreateArticelRequest model);
        Task<CreateArticelRequest> Create();
        Task<bool> Update(EditArticleRequest model);
        Task<bool> Delete(Guid id);
        Task<ArticleView> View(Guid id);
        Task<bool> AddComment(CommentView model);
    }
}
