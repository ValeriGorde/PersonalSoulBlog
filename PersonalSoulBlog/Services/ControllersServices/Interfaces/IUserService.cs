using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.User;

namespace PersonalSoulBlog.Services.ControllersServices.Interfaces
{
    public interface IUserService
    {
        Task<EditUserViewModel> GetUserById(string? id);
        List<User> GetAllUsers();
        Task<bool> CreateUser(CreateUserViewModel model);
        Task<bool> UpdateUser(EditUserViewModel model);
        Task<bool> DeleteUser(string id);
    }
}
