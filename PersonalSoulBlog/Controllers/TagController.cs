using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Tags;

namespace PersonalSoulBlog.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
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
            if (ModelState.IsValid)
            {
                await _tagService.CreateTag(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// Метод по получению тега по id для редактирования
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagService.GetTagById(id);

            if(tag != null)
            {
                return View(tag);
            }

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Метод по редактированию тега
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest model)
        {
            if(ModelState.IsValid)
            {
                var result = await _tagService.UpdateTag(model);

                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
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
