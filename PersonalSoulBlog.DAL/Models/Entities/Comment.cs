namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class Comment : BaseModel
    {
        public string Text { get; set; }

        // привязка комментария к пользователю
        public int UserId { get; set; }
        public User User { get; set; }

        //привязка комментария к статье
        public int ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
