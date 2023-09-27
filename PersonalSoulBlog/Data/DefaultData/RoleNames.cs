namespace PersonalSoulBlog.Data.DefaultData
{
    /// <summary>
    /// Роли по умолчанию
    /// </summary>
    public class RoleNames
    {
        public const string Administrator = "Администратор";
        public const string Moderator = "Модератор";
        public const string User = "Пользователь";

        public static IEnumerable<string> AllRoles
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
