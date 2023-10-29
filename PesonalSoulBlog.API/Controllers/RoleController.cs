using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Roles;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PesonalSoulBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        /// <summary>
        /// Вывод всех ролей
        /// </summary>
        /// <returns></returns>
        [Route("GetRoles")]
        [HttpGet]
        public IEnumerable<Role> Index()
        {
            var rolesList = _roleService.GetAllRoles();

            return rolesList;
        }

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [Route("CreateRole")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.CreateRole(model);
                if (result)
                {
                    return StatusCode(201);
                }
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Редактирование роли
        /// </summary>
        /// <returns></returns>
        [Route("EditRole")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleService.EditRole(model);
                if (result)
                {
                    return StatusCode(201);
                }
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteRole")]
        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _roleService.DeleteRole(id);
            return StatusCode(201);
        }

    }
}
