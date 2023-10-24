using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Articles
{
    public class CreateArticelRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<TagForArticleRequest> Tags { get; set; } = new List<TagForArticleRequest>();
    }
}
