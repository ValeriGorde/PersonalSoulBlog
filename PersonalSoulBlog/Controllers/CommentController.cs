using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Comments;

namespace PersonalSoulBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentService commentService, ILogger<CommentController> logger)
        {
            _commentService = commentService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var commentsList = await _commentService.GetAllComments();
            return View(commentsList);
        }


        /// <summary>
        /// Получение комментария по id для редактирования
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var comment = await _commentService.GetCommentById(id);

                if (comment != null)
                {
                    return View(comment);
                }

                return View("SmthGoesWrong");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }            
        }

        /// <summary>
        /// Метод по редактированию комментария
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var articleId = await _commentService.UpdateComment(model);

                    if (articleId != Guid.Empty)
                    {
                        return RedirectToAction("View", "Article", new { id = articleId });
                    }
                }

                return View("SmthGoesWrong");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        /// <summary>
        /// Метод по удалению комментария
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var articleId = await _commentService.DeleteComment(id);

            if (articleId != Guid.Empty)
            {
                return RedirectToAction("View", "Article", new {id = articleId});
            }

            // ошибка
            return View("View", "Article");
        }
    }
}
