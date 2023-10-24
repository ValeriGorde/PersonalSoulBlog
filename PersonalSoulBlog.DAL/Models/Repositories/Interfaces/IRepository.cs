using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalSoulBlog.DAL.Models.Repositories.Interfaces
{
    /// <summary>
    /// Базовый интерфейс
    /// </summary>
    public interface IRepository<T> where T : class
    {
        Task<T> GetById(Guid id);
        Task<List<T>> GetAllTags();
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
