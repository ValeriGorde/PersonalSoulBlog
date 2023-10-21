using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Comments;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IHttpContextAccessor _httpContext;

        public ArticleService(IMapper mapper, ApplicationDbContext context, 
            UserManager<User> userManager, IHttpContextAccessor httpContext)
        {
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _httpContext = httpContext;
        }


        public async Task Create(CreateArticelViewModel model)
        {

            var allArticleTags = _mapper.Map<List<TagForArticleViewModel>>(model.Tags);

            var article = _mapper.Map<Article>(model);
            article.Tags.Clear();

            foreach (var articleTag in allArticleTags)
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

            if (article != null)
            {
                _context.Articles.Remove(article);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await _context.Articles.Include(a => a.Tags)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<ArticleViewModel> GetArticleById(int id)
        {
            if (id == 0)
                return null;

            var article = await _context.Articles.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);

            if (article != null)
            {
                var newArticle = _mapper.Map<ArticleViewModel>(article);
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

                foreach (var tag in model.Tags)
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

        public async Task<ArticleViewModel> View(int id)
        {
            var article = await _context.Articles
                .Include(t => t.Tags)
                .Include(u => u.User)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<ArticleViewModel>(article);

            return viewModel;
        }

        public async Task<bool> AddComment(CommentViewModel model)
        {
            var comment = _mapper.Map<Comment>(model);

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
