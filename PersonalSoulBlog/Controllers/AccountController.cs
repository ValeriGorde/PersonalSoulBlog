using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.Account;

namespace PersonalSoulBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Регистраци пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _accountService.Register(model);
            if (result.Success)
            {
                return RedirectToAction("Login");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl});
        }


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var returnUrl = Url.Action("Index", "Home"); ;
                var result = await _accountService.Login(model, returnUrl);
                if (result.Success)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", result.ErrorMessage);
            }
            return View(model);
        }

        /// <summary>
        /// Выход из аккуанта
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Home");   
        }


    }
}
