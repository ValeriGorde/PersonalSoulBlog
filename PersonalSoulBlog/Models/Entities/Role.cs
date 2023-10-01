using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.Models.Entities
{
    public class Role: IdentityRole
    {
        public string Description { get; set; }
    }
}
