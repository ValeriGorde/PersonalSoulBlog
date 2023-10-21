using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Comments;

namespace PersonalSoulBlog.Services.ControllersServices.Interfaces
{
    public interface IArticleService
    {
        Task<List<Article>> GetAllArticles();
        Task<ArticleViewModel> GetArticleById(int id);
        Task Create(CreateArticelViewModel model);
        CreateArticelViewModel Create();
        Task<bool> Update(EditArticleViewModel model);
        Task<bool> Delete(int id);
        Task<ArticleViewModel> View(int id);
        Task<bool> AddComment(CommentViewModel model);
    }
}
