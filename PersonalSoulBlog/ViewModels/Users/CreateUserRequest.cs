using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.ViewModels.Users
{
    public class CreateUserRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string Password { get; set; }
        public List<RoleForUserRequest>? Roles { get; set; } = new List<RoleForUserRequest>();
    }
}
