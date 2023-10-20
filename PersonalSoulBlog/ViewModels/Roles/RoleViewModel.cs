using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.ViewModels.Roles
{
    // Модель для управления ролями
    public class RoleViewModel
    {
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public RoleViewModel()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
