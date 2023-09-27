using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Models.Entities;

namespace PersonalSoulBlog.Data.DefaultData
{
    /// <summary>
    /// Класс для создания ролей и пользователей по умолчанию
    /// </summary>
    public static class SeedData
    {
        public static async Task EnsureSeedData(IServiceProvider provider)
        {
            var roleMgr = provider.GetRequiredService<RoleManager<IdentityRole>>();
            foreach (var roleName in RoleNames.AllRoles)
            {
                var role = roleMgr.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    var result = roleMgr.CreateAsync(new IdentityRole { Name = roleName }).Result;
                    if (!result.Succeeded)
                    {
                        throw new Exception(result.Errors.First().Description);
                    }
                }
            }

            var userMgr = provider.GetRequiredService<UserManager<User>>();

            var adminResult = await userMgr.CreateAsync(DefaultUsers.Administrator, "admin1");
            var moderatorResult = await userMgr.CreateAsync(DefaultUsers.Moderator, "moderator1");
            var userResult = await userMgr.CreateAsync(DefaultUsers.User, "user1");

            if (adminResult.Succeeded || moderatorResult.Succeeded || userResult.Succeeded)
            {
                var admin = await userMgr.FindByNameAsync(DefaultUsers.Administrator.Email);
                var moderator = await userMgr.FindByNameAsync(DefaultUsers.Moderator.Email);
                var user = await userMgr.FindByNameAsync(DefaultUsers.User.Email);

                await userMgr.AddToRoleAsync(admin, RoleNames.Administrator);
                await userMgr.AddToRoleAsync(moderator, RoleNames.Moderator);
                await userMgr.AddToRoleAsync(user, RoleNames.User);
            }
        }
    }
}
