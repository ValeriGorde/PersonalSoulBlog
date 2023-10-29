using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Users;
using System.Security.Cryptography;

namespace PersonalSoulBlog.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        /// <summary>
        /// Вывод всех пользователей
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var usersList = await _userService.GetAllUsers();
            return View(usersList);
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
        public async Task<IActionResult> Edit(EditUserRequest model)
        {
            try
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
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        /// <summary>
        /// Удаление пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string? id)
        {
            if(id != null)
            {
                await _userService.DeleteUser(id);
                return RedirectToAction("Index");
            }
            return View("SmthGoesWrong");           
        }
    }
}
