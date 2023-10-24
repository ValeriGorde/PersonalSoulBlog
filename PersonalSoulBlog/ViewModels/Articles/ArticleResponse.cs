using PersonalSoulBlog.ViewModels.Comments;
using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class ArticleResponse
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }   
        public string Description { get; set; }
        public UserResponse Author { get; set; }
        public List<CommentResponse> Comments { get; set; }
        public List<TagForArticleRequest> Tags { get; set; } = new List<TagForArticleRequest>();
    }
}
