using Microsoft.AspNetCore.Identity;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.DAL.Data.DefaultData
{
    /// <summary>
    /// Пользователи по умолчанию
    /// </summary>
    public static class DefaultUsers
    {
        public static readonly User Administrator = new User
        {
            FirstName = "Админ",
            LastName = "Администратович",
            Email = "admin@mail.ru",
            EmailConfirmed = true,
            UserName = "admin@mail.ru"
        };

        public static readonly User Moderator = new User
        {
            FirstName = "Модер",
            LastName = "Модератович",
            Email = "moderator@mail.ru",
            EmailConfirmed = true,
            UserName = "moderator@mail.ru"
        };

        public static readonly User User = new User
        {
            FirstName = "Юзер",
            LastName = "Юзерович",
            Email = "user@mail.ru",
            EmailConfirmed = true,
            UserName = "user@mail.ru"
        };

        public static IEnumerable<User> AllUsers
        {
            get
            {
                yield return Administrator;
                yield return Moderator;
                yield return User;
            }
        }
    }
}
