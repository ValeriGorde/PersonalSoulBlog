using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Comments;

namespace PersonalSoulBlog.Services.Contracts.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticles();
        Task<ArticleResponse> GetArticleById(Guid id);
        Task Create(CreateArticelRequest model);
        Task<CreateArticelRequest> Create();
        Task<bool> Update(EditArticleRequest model);
        Task<bool> Delete(Guid id);
        Task<ArticleResponse> View(Guid id);
        Task<bool> AddComment(CommentResponse model);
    }
}
