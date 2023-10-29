using PersonalSoulBlog.BLL.ViewModels.Account;
using PersonalSoulBlog.Services.Contracts.Model;

namespace PersonalSoulBlog.BLL.Services.Contracts.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResult> Register(RegisterRequest model);
        Task<LoginResult> Login(LoginRequest model, string? url);
        Task Logout();
    }
}
