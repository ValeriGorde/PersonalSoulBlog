using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Comments;

namespace PersonalSoulBlog.API.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ArticleController> _logger;

        public ArticleController(IArticleService articleService, UserManager<User> userManager, 
            ILogger<ArticleController> logger)
        {
            _articleService = articleService;
            _userManager = userManager;
            _logger = logger;
            _logger.LogDebug("NLog встроен в ArticleController");
        }


        /// <summary>
        /// Вывод всех статей списком
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var articles = await _articleService.GetAllArticles();
            return View(articles);
        }

        /// <summary>
        /// Метод для создания статьи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var model = await _articleService.Create();

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticelRequest model)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                    model.User = currentUser;

                if (ModelState.IsValid)
                {
                    await _articleService.Create(model);
                    return RedirectToAction("Index");
                }

                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var article = await _articleService.GetArticleById(id);

                if (article != null)
                {
                    return View(article);
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _articleService.Update(model);
                    if (result)
                        return RedirectToAction("Index");
                }
                return View(model);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _articleService.Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }


        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            try
            {
                var article = await _articleService.View(id);

                if (article != null)
                    return View(article);

                return View("NotFound");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(CommentResponse model)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                    model.User = currentUser;

                if (ModelState.IsValid)
                {
                    var result = await _articleService.AddComment(model);

                    if (result)
                        return RedirectToAction("View", "Article", new { id = model.ArticleId });
                }

                return View("SmthGoesWrong");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }            
        }

    }
}
