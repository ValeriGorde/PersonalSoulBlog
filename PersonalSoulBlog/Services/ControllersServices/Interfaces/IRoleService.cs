using Microsoft.AspNetCore.Mvc.ModelBinding;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Roles;

namespace PersonalSoulBlog.Services.ControllersServices.Interfaces
{
    public interface IRoleService
    {
        Task<bool> CreateRole(CreateRoleViewModel model);
        Task<bool> DeleteRole(string id);
        Task<bool> EditRole(EditRoleViewModel model);
        Task<EditRoleViewModel> GetRoleById(string? id);
        List<Role> GetAllRoles();
    }
}
