using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.Services.Contracts.Model;
using PersonalSoulBlog.ViewModels.Account;
using System;

namespace PersonalSoulBlog.Services.Contracts
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResult> Login(LoginRequest model, string? url)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (result.Succeeded)
            {
                // проверка на то, принадлежит ли url приложению
                if (!string.IsNullOrEmpty(url) && url.StartsWith("/"))
                {
                    return new LoginResult { Success = true, RedirectUrl = url };
                }
                else
                    return new LoginResult { Success = true, RedirectUrl = "/Home/Index" };
            }
            else
                return new LoginResult { Success = false, ErrorMessage = "Неправильный логин и(или) пароль" };
            
        }

        public async Task Logout()
        {
            // удаление аутентификационных куки
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResult> Register(RegisterRequest model)
        {
            var user = _mapper.Map<User>(model);

            // добавляем пользователя
            var result = await _userManager.CreateAsync(user, model.Password);
            // присваиваем по умолчанию роль - пользователь
            await _userManager.AddToRoleAsync(user, "Пользователь");

            if (result.Succeeded)
            {
                // установка куки 
                await _signInManager.SignInAsync(user, false);
                return new RegisterResult { Success = true };
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return new RegisterResult { Success = false, Errors = errors };
            }
        }
    }
}
