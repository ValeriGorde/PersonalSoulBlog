using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class EditArticleViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<TagForArticleViewModel> Tags { get; set; } = new List<TagForArticleViewModel>();
    }
}
