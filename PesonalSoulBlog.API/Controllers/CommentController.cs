using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Comments;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PesonalSoulBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <returns></returns>
        [Route("GetComments")]
        [HttpGet]
        public async Task<IEnumerable<Comment>> Index()
        {
            var commentsList = await _commentService.GetAllComments();
            return commentsList;
        }


        /// <summary>
        /// Метод по редактированию комментария
        /// </summary>
        /// <returns></returns>
        [Route("EditComment")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditCommentRequest model)
        {
            if (ModelState.IsValid)
            {
                var articleId = await _commentService.UpdateComment(model);

                if (articleId != Guid.Empty)
                {
                    return StatusCode(201);
                }
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Метод по удалению комментария
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteComment")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var articleId = await _commentService.DeleteComment(id);

            if (articleId != Guid.Empty)
            {
                return StatusCode(201);
            }

            return StatusCode(500);
        }
    }
}
