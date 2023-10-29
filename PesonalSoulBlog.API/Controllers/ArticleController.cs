using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Articles;
using PersonalSoulBlog.BLL.ViewModels.Comments;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PesonalSoulBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly UserManager<User> _userManager;

        public ArticleController(IArticleService articleService, UserManager<User> userManager)
        {
            _articleService = articleService;
            _userManager = userManager;
        }

        /// <summary>
        /// Вывод всех статей списком
        /// </summary>
        /// <returns></returns>
        [Route("GetArticles")]
        [HttpGet]
        public async Task<IEnumerable<Article>> Index()
        {
            var articles = await _articleService.GetAllArticles();

            return articles;
        }

        /// <summary>
        /// Создание статьи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("CreateArticle")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateArticelRequest model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
                model.User = currentUser;

            if (ModelState.IsValid)
            {
                await _articleService.Create(model);
                return StatusCode(201);
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Редактирование статьи
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("EditArticle")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditArticleRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _articleService.Update(model);
                if (result)
                    return StatusCode(201);
            }
            return StatusCode(500);
        }

        /// <summary>
        /// Удаление статьи
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteArticle")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _articleService.Delete(id);
            if (result)
            {
                return StatusCode(201);
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Добавление комментария к статье
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("AddComment")]
        [HttpPost]
        public async Task<IActionResult> AddComment(CommentView model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
                model.User = currentUser;

            if (ModelState.IsValid)
            {
                var result = await _articleService.AddComment(model);

                if (result)
                    return StatusCode(201);
            }

            return StatusCode(500);
        }
    }
}
