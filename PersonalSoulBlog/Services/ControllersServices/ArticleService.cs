using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.Articles;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public ArticleService(IMapper mapper, ApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task Create(CreateArticelViewModel model)
        {
            //var article = _mapper.Map<Article>(model);

            var article = new Article
            {
                Title = model.Title,
                Description = model.Description,
                Tags = model.SelectedTagsIds.Select(tagId => new Tag { Id = tagId}).ToList(),
                UserId = model.UserId
            };
            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public CreateArticelViewModel Create()
        {
            return new CreateArticelViewModel
            {
                Tags = _context.Tags.ToList()
            };
        }

        public async Task<bool> Delete(int id)
        {
            var article = await _context.Articles.FindAsync(id);

            if(article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public List<Article> GetAllArticles()
        {
            return _context.Articles.ToList();
        }

        public async Task<EditArticleViewModel> GetArticleById(int id)
        {
            if (id == 0)
                return null;

            var article = await _context.Articles.FindAsync(id);

            if (article != null)
            {
                var newArticle = _mapper.Map<EditArticleViewModel>(article);
                return newArticle;
            }

            return null;
        }

        public async Task<bool> Update(EditArticleViewModel model)
        {
            var article = await _context.Articles.FindAsync(model);

            if (article != null)
            {
                _context.Articles.Update(article);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
