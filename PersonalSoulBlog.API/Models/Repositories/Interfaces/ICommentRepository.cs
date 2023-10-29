using PersonalSoulBlog.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.DAL.Models.Repositories.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task<List<Comment>> GetAllComments();
    }
}
