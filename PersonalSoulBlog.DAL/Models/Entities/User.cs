using Microsoft.AspNetCore.Identity;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
