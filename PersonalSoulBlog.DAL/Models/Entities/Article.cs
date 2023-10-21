using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    // Модель статьи
    public class Article : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        // привязка статьи к пользователю
        public int UserId { get; set; }
        public User User { get; set; }

        // привязка статьи к комментариям
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // связь многие ко многим с тегами
        [InverseProperty("Articles")]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
