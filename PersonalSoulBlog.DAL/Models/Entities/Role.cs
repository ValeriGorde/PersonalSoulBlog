using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace PersonalSoulBlog.DAL.Models.Entities
{
    public class Role: IdentityRole
    {
        [Display(Name = "Описание")]
        public string? Description { get; set; }
    }
}
