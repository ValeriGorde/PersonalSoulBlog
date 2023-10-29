using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Tags;

namespace PersonalSoulBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagService tagService, UserManager<User> userManager,
            ILogger<TagController> logger)
        {
            _tagService = tagService;
            _userManager = userManager;
            _logger = logger;
        }

        /// <summary>
        /// Представление для всех тэгов
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var tagsList = await _tagService.GetAllTags();
            return View(tagsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        /// <summary>
        /// Метод для создания тега
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagRequest model)
        {
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser != null)
                    model.User = currentUser;

                if (ModelState.IsValid)
                {
                    await _tagService.CreateTag(model);
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

        /// <summary>
        /// Метод по получению тега по id для редактирования
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var tag = await _tagService.GetTagById(id);

                if (tag != null)
                {
                    return View(tag);
                }

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }

        /// <summary>
        /// Метод по редактированию тега
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _tagService.UpdateTag(model);

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
        /// Метод по удалению тега
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _tagService.DeleteTag(id);

            if (result)
            {
                return RedirectToAction("Index");
            }

            return View(result);
        }        
    }
}
