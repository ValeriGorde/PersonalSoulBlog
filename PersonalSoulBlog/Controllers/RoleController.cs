using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Models.Entities;
using PersonalSoulBlog.ViewModels.Roles;

namespace PersonalSoulBlog.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class RoleController : Controller
    {
        private readonly IMapper _mapper;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleController(IMapper mapper, RoleManager<Role> roleManager, UserManager<User> userManager)
        {
            _mapper = mapper;
            _roleManager = roleManager;
            _userManager = userManager;
        }


        /// <summary>
        /// Вывод всех ролей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View(_roleManager.Roles.ToList());
        }


        /// <summary>
        /// Представление для создания ролей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Создание роли
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = _mapper.Map<Role>(model);
                var result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        /// <summary>
        /// Получение роли по id для редактирования
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == string.Empty || id == null)
            {
                return NotFound();
            }

            var role = await _roleManager.FindByIdAsync(id);
            if(role != null)
            {
                var newRole = _mapper.Map<EditRoleViewModel>(role);
                return View(newRole);
            }
            return RedirectToAction("Index");   
        }


        /// <summary>
        /// Редактирование роли
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);
                if(role != null)
                {
                    var newRole = _mapper.Map<Role>(model);

                    role.Name = newRole.Name;
                    role.Description = newRole.Description;

                    var result = await _roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                        ModelState.AddModelError("", "Ошибка");
                }            
            }

            return View(model);
        }

        /// <summary>
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }
            return RedirectToAction("Index");
        }
    }
}
