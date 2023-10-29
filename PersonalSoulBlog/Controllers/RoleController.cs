using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Roles;

namespace PersonalSoulBlog.Controllers
{
    //[Authorize(Roles = "Администратор")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }

        /// <summary>
        /// Вывод всех ролей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            var rolesList = _roleService.GetAllRoles();
            

            return View(rolesList);
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
        public async Task<IActionResult> Create(CreateRoleRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _roleService.CreateRole(model);
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
        /// Получение роли по id для редактирования
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {            
            var role = await _roleService.GetRoleById(id);
            if (role != null)
            {
                return View(role);
            }

            return RedirectToAction("Index");            
        }


        /// <summary>
        /// Редактирование роли
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditRoleRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _roleService.EditRole(model);
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
        /// Удаление роли
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _roleService.DeleteRole(id);
            return RedirectToAction("Index");
        }
    }
}
