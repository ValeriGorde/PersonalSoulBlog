using PersonalSoulBlog.BLL.ViewModels.Users;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.Services.Contracts.Interfaces
{
    public interface IUserService
    {
        Task<EditUserRequest> GetUserById(string? id);
        Task<List<User>> GetAllUsers();
        Task<CreateUserRequest> CreateUser();
        Task<bool> CreateUser(CreateUserRequest model);
        Task<bool> UpdateUser(EditUserRequest model);
        Task<bool> DeleteUser(string id);
    }
}
