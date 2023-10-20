using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.User;
using System.Linq;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public UserService(IMapper mapper, UserManager<User> userManager,
            SignInManager<User> signInManager, RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }


        public async Task<bool> CreateUser(CreateUserViewModel model)
        {
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                foreach (var role in model.Roles)
                {
                    if (role.IsSelected)
                    {
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                    }
                }

                await _signInManager.SignInAsync(user, false);
                return true;
            }

            return false;
        }

        public async Task<CreateUserViewModel> CreateUser()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var model = new CreateUserViewModel
            {
                Roles = roles.Select(role => new RoleForUserViewModel
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                }).ToList()
            };

            return model;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }

            return false;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in roles)
                {
                    var newRole = new Role { Name = role };
                    await _userManager.AddToRoleAsync(user, newRole.Name);
                }
            }

            return users;
        }

        public async Task<EditUserViewModel> GetUserById(string? id)
        {
            if (id == null || id == string.Empty)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(id);   

            if (user != null)
            {
                var newUser = _mapper.Map<EditUserViewModel>(user);

                var allRoles = _roleManager.Roles.ToList();
                var roles = await _userManager.GetRolesAsync(user);

                foreach (var role in allRoles)
                {
                    if (roles.FirstOrDefault() == role.Name)
                    {
                        var roleIsSelected = new RoleForUserViewModel
                        {
                            IsSelected = true,
                            RoleName = role.Name
                        };
                        newUser.Roles.Add(roleIsSelected);
                    }
                    else
                    {
                        var roleIsntSelected = new RoleForUserViewModel
                        {
                            IsSelected = false,
                            RoleName = role.Name
                        };
                        newUser.Roles.Add(roleIsntSelected);
                    }     
                }                
                
                return newUser;
            }

            return null;
        }

        /// <summary>
        /// Метод по обновлению данных о пользователе (пока пользователь может иметь только одну роль!!)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> UpdateUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if (user != null)
            {
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;

                // удаляем предыдущие роли пользователя прежде чем добавить новую
                var currentRole = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, currentRole);

                foreach(var role in model.Roles)
                {
                    if (role.IsSelected)
                        await _userManager.AddToRoleAsync(user, role.RoleName);
                }                

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
