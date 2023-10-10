using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.User;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CreateUser(CreateUserViewModel model)
        {
            var user = _mapper.Map<User>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            await _userManager.AddToRoleAsync(user, model.Role.Name);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return true;
            }

            return false;
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

        public List<User> GetAllUsers()
        {
            return _userManager.Users.ToList();
        }

        public async Task<EditUserViewModel> GetUserById(string? id)
        {
            if(id == null || id == string.Empty)
            {
                return null;
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var newUser = _mapper.Map<EditUserViewModel>(user);
                return newUser;
            }

            return null;
        }

        public async Task<bool> UpdateUser(EditUserViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);
            if(user != null)
            {
                var newUser = _mapper.Map<User>(user);

                user.FirstName = newUser.FirstName;
                user.LastName = newUser.LastName;
                user.Email = newUser.Email;

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
