using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Account;

namespace PersonalSoulBlog.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
            _logger.LogDebug("NLog встроен в AccountController");
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
        public async Task<IActionResult> Register(RegisterRequest model)
        {
            try
            {
                var result = await _accountService.Register(model);
                if (result.Success)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);

                // ошибка на стороне сервера
                return StatusCode(500);
            }
            
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginRequest { ReturnUrl = returnUrl});
        }


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var returnUrl = Url.Action("Index", "Article"); ;
                    var result = await _accountService.Login(model, returnUrl);
                    if (result.Success)
                    {
                        return RedirectToAction("Index", "Article");
                    }
                    else
                        ModelState.AddModelError("", result.ErrorMessage);
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
        /// Выход из аккуанта
        /// </summary>
        /// <returns></returns>
        [Route("Logout")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _accountService.Logout();
            return RedirectToAction("Index", "Article");   
        }
    }
}
