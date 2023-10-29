using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    public class TagRepository : RepositoryBase<Tag>, ITagRepository
    {
        public TagRepository(ApplicationDbContext context) : base(context)
        {
            
        }
        public async Task<List<Tag>> GetAllTags()
        {
            return await _context.Tags
                 .Include(a => a.Articles)
                 .Include(u => u.User)
                 .ToListAsync();
        }
    }
}
