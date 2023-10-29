using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    /// <summary>
    /// Модель статьи
    /// </summary>
    public class Article : BaseModel
    {
        [Required(ErrorMessage = "Поле имени является обязательным")]
        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле содержания является обязательным")]
        [Display(Name = "Содержание статьи")]
        public string Description { get; set; }

        // привязка статьи к пользователю
        public User User { get; set; }

        // привязка статьи к комментариям
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // связь многие ко многим с тегами
        [InverseProperty("Articles")]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
