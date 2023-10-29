using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.DAL.Models.Repositories
{
    public class ArticleRepository : RepositoryBase<Article>, IArticleRepository
    {
        public ArticleRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await _context.Articles
                .Include(t => t.Tags)
                .Include(u => u.User)
                .Include(c => c.Comments)
                .ToListAsync();
        }

        public async Task<Article> GetArticleById(Guid id)
        {
            return await _context.Articles
                .Include(t => t.Tags)
                .Include(c => c.Comments)
                .Include(u => u.User)
                .FirstOrDefaultAsync(a => a.Id == id) ?? throw new Exception($"Статья с id: {id} не найдена");
        }
    }
}
