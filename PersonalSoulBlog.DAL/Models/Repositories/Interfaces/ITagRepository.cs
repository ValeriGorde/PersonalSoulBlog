using PersonalSoulBlog.DAL.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.DAL.Models.Repositories.Interfaces
{
    /// <summary>
    /// Интерфейс для реализации методов crud исклчительно для тегов
    /// </summary>
    public interface ITagRepository: IRepository<Tag>
    {
        Task<List<Tag>> GetAll();
    }
}
