using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class Role: IdentityRole
    {
        public string? Description { get; set; }
    }
}
