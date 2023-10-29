using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Tags;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PesonalSoulBlog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly UserManager<User> _userManager;

        public TagController(ITagService tagService, UserManager<User> userManager)
        {
            _tagService = tagService;
            _userManager = userManager;
        }

        /// <summary>
        /// Вывод всех тегов
        /// </summary>
        /// <returns></returns>
        /// 
        [Route("GetTags")]
        [HttpGet]
        public async Task<IEnumerable<Tag>> Index()
        {
            var tagsList = await _tagService.GetAllTags();
            return tagsList;
        }

        /// <summary>
        /// Метод для создания тега
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("CreateTag")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagRequest model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser != null)
                model.User = currentUser;

            if (ModelState.IsValid)
            {
                await _tagService.CreateTag(model);
                return StatusCode(201);
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Метод по редактированию тега
        /// </summary>
        /// <returns></returns>
        [Route("EditTag")]
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest model)
        {
            if (ModelState.IsValid)
            {
                var result = await _tagService.UpdateTag(model);

                if (result)
                {
                    return StatusCode(201);
                }
            }

            return StatusCode(500);
        }

        /// <summary>
        /// Метод по удалению тега
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("DeleteTag")]
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tagService.DeleteTag(id);

            if (result)
            {
                return StatusCode(201);
            }

            return StatusCode(500);
        }
    }
}
