using PersonalSoulBlog.BLL.ViewModels.Articles;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.ViewModels.Comments
{
    public class CommentView
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public User? User { get; set; }
        public Guid ArticleId { get; set; }
        public ArticleView? Article { get; set; }
    }
}
