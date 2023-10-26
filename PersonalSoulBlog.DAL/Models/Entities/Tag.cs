using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    /// <summary>
    /// Модель тега
    /// </summary>
    public class Tag : BaseModel
    {
        public string Name { get; set; }

        // привязка тега к пользователю
        public User User { get; set; }

        // связь многие ко многим со статьями

        [InverseProperty("Tags")]
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
