using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class Comment : BaseModel
    {
        [Required(ErrorMessage = "Поле текста является обязательным")]
        [Display(Name = "Текст")]
        public string Text { get; set; }

        // привязка комментария к пользователю

        public User User { get; set; }

        //привязка комментария к статье
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
