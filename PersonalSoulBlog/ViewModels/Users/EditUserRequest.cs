using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.ViewModels.Users
{
    public class EditUserRequest
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }
        public List<RoleForUserRequest>? Roles { get; set; } = new List<RoleForUserRequest>();
    }
}
