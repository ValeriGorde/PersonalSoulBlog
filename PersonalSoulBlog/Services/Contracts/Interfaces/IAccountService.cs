using PersonalSoulBlog.Services.Contracts.Model;
using PersonalSoulBlog.ViewModels.Account;

namespace PersonalSoulBlog.Services.Contracts.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> Register(RegisterRequest model);
        Task<LoginResult> Login(LoginRequest model, string? url);
        Task Logout();
    }
}
