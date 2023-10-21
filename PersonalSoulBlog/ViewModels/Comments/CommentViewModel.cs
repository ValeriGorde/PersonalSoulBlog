using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.ViewModels.Comments
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int UserId { get; set; } 
        public User? User { get; set; }
        public int ArticleId { get; set; }
        public ArticleViewModel? Article { get; set; }
    }
}
