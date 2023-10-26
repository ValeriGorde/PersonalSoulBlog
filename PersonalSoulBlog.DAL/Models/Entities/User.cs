using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        // один пользователь может оставлять множество комментариев
        public List<Comment> Comments { get; set; } = new List<Comment>();

        // у одного пользователя может быть несколько статей
        public List<Article> Articles { get; set; } = new List<Article>();
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
