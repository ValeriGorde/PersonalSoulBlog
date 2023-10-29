using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.BLL.ViewModels.Roles
{
    // Модель для управления ролями
    public class RoleView
    {
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        public RoleView()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
