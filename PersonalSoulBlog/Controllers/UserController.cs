﻿using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.User;

namespace PersonalSoulBlog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Вывод всех пользователей
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var usersList = _userService.GetAllUsers();
            return View(usersList);
        }


        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUser(model);

                if (result)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        /// <summary>
        /// Метод для получения пользователя по id (для редактирования)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            var user = await _userService.GetUserById(id);
            if(user != null)
            {
                return View(user);
            }

            return RedirectToAction("Index");            
        }

        /// <summary>
        /// Метод для редактирования пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUser(model);

                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        /// <summary>
        /// Удаление пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {
            await _userService.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}