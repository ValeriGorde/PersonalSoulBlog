using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.ViewModels.Users
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RoleForUserViewModel>? Roles { get; set; } = new List<RoleForUserViewModel>();
    }
}
