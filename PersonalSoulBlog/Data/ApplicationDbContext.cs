using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.Models.Entities;
using System.Reflection.Emit;

namespace PersonalSoulBlog.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Создание и инициализация ролей
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Пользователь", NormalizedName = "ПОЛЬЗОВАТЕЛЬ" },
                new IdentityRole { Name = "Администратор", NormalizedName = "АДМИНИСТРАТОР" },
                new IdentityRole { Name = "Модератор", NormalizedName = "МОДЕРАТОР" }
            );
        }
    }
}
