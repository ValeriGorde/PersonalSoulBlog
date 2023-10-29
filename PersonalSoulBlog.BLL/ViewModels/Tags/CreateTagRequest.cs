using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.ViewModels.Tags
{
    public class CreateTagRequest
    {
        public string Name { get; set; }
        public User? User { get; set; }
    }
}
