using PersonalSoulBlog.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.DAL.Models.Repositories.Interfaces
{
    public interface IArticleRepository: IRepository<Article>
    {
        Task<List<Article>> GetAllArticles();
        Task<Article> GetArticleById(Guid id);
    }
}
