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
            
            var allArticleTags = _mapper.Map<List<TagForArticleViewModel>>(model.Tags);

            var article = _mapper.Map<Article>(model);
            article.Tags.Clear();

            foreach(var articleTag in allArticleTags)
            {
                if (articleTag.IsSelected)
                {
                    var tag = await _context.Tags.FindAsync(articleTag.TagId);
                    if (tag != null)
                    {
                        article.Tags.Add(tag);
                    }
                }                
            }

            await _context.Articles.AddAsync(article);
            await _context.SaveChangesAsync();
        }

        public CreateArticelViewModel Create()
        {
            var tags = _context.Tags.ToList();
            var allTags = _mapper.Map<List<TagForArticleViewModel>>(tags);

            return new CreateArticelViewModel
            {
                Tags = allTags
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

        public async Task<List<Article>> GetAllArticles()
        {
            return await _context.Articles.Include(a => a.Tags).ToListAsync();
        }

        public async Task<EditArticleViewModel> GetArticleById(int id)
        {
            if (id == 0)
                return null;

            var article = await _context.Articles.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                var newArticle = _mapper.Map<EditArticleViewModel>(article);
                newArticle.Tags.Clear();

                var allTags = await _context.Tags.ToListAsync();

                foreach (var tag in allTags)
                {
                    var newTag = new TagForArticleViewModel
                    {
                        IsSelected = article.Tags.Any(t => t.Id == tag.Id),
                        TagId = tag.Id,
                        TagName = tag.Name
                    };
                    newArticle.Tags.Add(newTag);
                }

                return newArticle;
            }

            return null;
        }

        public async Task<bool> Update(EditArticleViewModel model)
        {
            var newArticle = _mapper.Map<Article>(model);
            var article = await _context.Articles.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == newArticle.Id);

            if (article != null)
            {
                article.Id = model.Id;
                article.Title = model.Title;
                article.Description = model.Description;

                // отчищаем предыдущие теги у статьи    
                article.Tags.Clear();

                foreach(var tag in model.Tags)
                {
                    if (tag.IsSelected)
                    {
                        var existingTag = await _context.Tags.FindAsync(tag.TagId);
                        if (existingTag != null)
                        {
                            article.Tags.Add(existingTag);
                        }
                    }                        
                }

                _context.Articles.Update(article);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
    }
}
