using PersonalSoulBlog.Services.ControllersServices.Model;
using PersonalSoulBlog.ViewModels.Account;

namespace PersonalSoulBlog.Services.ControllersServices.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> Register(RegisterViewModel model);
        Task<LoginResult> Login(LoginViewModel model, string? url);
        Task Logout();
    }
}
