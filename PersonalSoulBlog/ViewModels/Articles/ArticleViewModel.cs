using PersonalSoulBlog.ViewModels.Comments;
using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
        public UserViewModel Author { get; set; }
        public List<CommentViewModel> Comments { get; set; }
        public List<TagForArticleViewModel> Tags { get; set; } = new List<TagForArticleViewModel>();
    }
}
