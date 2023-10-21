using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Users
{
    public class UserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Role> AllRoles { get; set; } = new List<Role>();
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
