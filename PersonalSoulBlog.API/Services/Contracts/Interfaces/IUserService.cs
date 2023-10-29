using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Users;

namespace PersonalSoulBlog.Services.Contracts.Interfaces
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
