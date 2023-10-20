using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Articles;

namespace PersonalSoulBlog.Services.ControllersServices.Interfaces
{
    public interface IArticleService
    {
        List<Article> GetAllArticles();
        Task<EditArticleViewModel> GetArticleById(int id);
        Task Create(CreateArticelViewModel model);
        CreateArticelViewModel Create();
        Task<bool> Update(EditArticleViewModel model);
        Task<bool> Delete(int id);
    }
}
