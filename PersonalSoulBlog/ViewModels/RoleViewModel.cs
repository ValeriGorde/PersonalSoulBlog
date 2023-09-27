using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.ViewModels
{
    // Модель для управления ролями
    public class RoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public RoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
