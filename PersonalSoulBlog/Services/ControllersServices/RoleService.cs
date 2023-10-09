using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.Roles;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<bool> CreateRole(CreateRoleViewModel model)
        {
            var role = _mapper.Map<Role>(model);
            var result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return false;
        }

        public async Task<bool> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);
            if (role != null)
            {
                var newRole = _mapper.Map<Role>(model);

                role.Name = newRole.Name;
                role.Description = newRole.Description;

                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public List<Role> GetAllRoles()
        {
            return _roleManager.Roles.ToList();
        }

        public async Task<EditRoleViewModel> GetRoleById(string? id)
        {
            if (id == string.Empty || id == null)
            {
                return null;
            }

            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var newRole = _mapper.Map<EditRoleViewModel>(role);
                return newRole;
            }
            return null;
        }
    }
}
