using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.DAL.Models.Repositories.Interfaces
{
    public interface IArticleRepository: IRepository<Article>
    {
        Task<List<Article>> GetAllArticles();
        Task<Article> GetArticleById(Guid id);
    }
}
