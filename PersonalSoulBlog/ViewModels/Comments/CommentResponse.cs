using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Articles;

namespace PersonalSoulBlog.ViewModels.Comments
{
    public class CommentResponse
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User? User { get; set; }
        public Guid ArticleId { get; set; }
        public ArticleResponse? Article { get; set; }
    }
}
