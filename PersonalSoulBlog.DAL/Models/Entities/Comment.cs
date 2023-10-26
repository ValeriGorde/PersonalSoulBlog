using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }

        // привязка комментария к пользователю

        public User User { get; set; }

        //привязка комментария к статье
        public Guid ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
