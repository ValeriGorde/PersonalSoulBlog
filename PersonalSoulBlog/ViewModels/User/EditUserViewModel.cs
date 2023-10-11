using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.User
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; } 
    }
}
