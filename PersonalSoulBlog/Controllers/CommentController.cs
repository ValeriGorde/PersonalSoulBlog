using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.Services.ControllersServices;
using PersonalSoulBlog.ViewModels.Comments;
using PersonalSoulBlog.ViewModels.Tags;

namespace PersonalSoulBlog.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
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
            var comment = await _commentService.GetCommentById(id);

            if (comment != null)
            {
                return View(comment);
            }

            // тут ошибку добавить
            return RedirectToAction("View", "Article");
        }

        /// <summary>
        /// Метод по редактированию комментария
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentRequest model)
        {
            if (ModelState.IsValid)
            {
                var articleId = await _commentService.UpdateComment(model);

                if (articleId != Guid.Empty)
                {
                    return RedirectToAction("View", "Article", new { id = articleId });
                }
            }

            // ошибку обработать
            return View("Edit", "Comment");
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
