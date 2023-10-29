using PersonalSoulBlog.BLL.ViewModels.Roles;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.Services.Contracts.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRole(CreateRoleRequest model);
        Task<bool> DeleteRole(string id);
        Task<bool> EditRole(EditRoleRequest model);
        Task<EditRoleRequest> GetRoleById(string? id);
        List<Role> GetAllRoles();
    }
}
