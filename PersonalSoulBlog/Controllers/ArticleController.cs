using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.Articles;

namespace PersonalSoulBlog.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        /// <summary>
        /// Вывод всех статей списком
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var articles = _articleService.GetAllArticles();
            return View(articles);
        }

        /// <summary>
        /// Метод для создания статьи
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Create()
        {
            var model = _articleService.Create();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateArticelViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _articleService.Create(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var article = await _articleService.GetArticleById(id);

            if (article != null)
            {
                return View(article);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _articleService.Update(model);
                if (result)
                    return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _articleService.Delete(id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }

    }
}
