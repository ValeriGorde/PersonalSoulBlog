using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Users;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PesonalSoulBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
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
        [Route("GetUsers")]
        [HttpGet]
        public async Task<IEnumerable<User>> Index()
        {
            var usersList = await _userService.GetAllUsers();
            return usersList;
        }

        /// <summary>
        /// Метод для редактирования пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("EditUser")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.UpdateUser(model);

                if (result)
                {
                    return StatusCode(201);
                }
            }

            return StatusCode(500);
        }


        /// <summary>
        /// Удаление пользователя по Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteUser")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string? id)
        {
            if (id != null)
            {
                await _userService.DeleteUser(id);
                return StatusCode(201);
            }
            return StatusCode(500);
        }
    }
}
