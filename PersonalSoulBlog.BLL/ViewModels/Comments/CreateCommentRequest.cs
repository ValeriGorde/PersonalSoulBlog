using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.ViewModels.Comments
{
    /// <summary>
    /// Запрос для создания комментария
    /// </summary>
    public class CreateCommentRequest
    {
        public string Text { get; set; }
        public User? User { get; set; }
        public Guid ArticleId { get; set; }
    }
}
