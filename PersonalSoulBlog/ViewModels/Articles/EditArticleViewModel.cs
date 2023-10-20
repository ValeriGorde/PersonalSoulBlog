using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
