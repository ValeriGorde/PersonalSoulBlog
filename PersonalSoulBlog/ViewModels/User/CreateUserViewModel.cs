using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.User
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
