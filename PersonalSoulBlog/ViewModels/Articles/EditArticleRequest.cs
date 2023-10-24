using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class EditArticleRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TagForArticleRequest> Tags { get; set; } = new List<TagForArticleRequest>();
    }
}
