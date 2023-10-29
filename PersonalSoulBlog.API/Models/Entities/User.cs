using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "Поле имени является обязательным")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле фамилии является обязательным")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Поле Email является обязательным")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        // один пользователь может оставлять множество комментариев
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // у одного пользователя может быть несколько статей
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
