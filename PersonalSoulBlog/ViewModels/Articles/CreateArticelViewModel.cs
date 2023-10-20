using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class CreateArticelViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
        [BindProperty]
        public List<int> SelectedTagsIds { get; set; }
    }
}
