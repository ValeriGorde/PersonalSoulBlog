using Microsoft.AspNetCore.Mvc.ModelBinding;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Roles;

namespace PersonalSoulBlog.Services.Contracts.Interfaces
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
