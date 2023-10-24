using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Articles;
using PersonalSoulBlog.ViewModels.Comments;

namespace PersonalSoulBlog.Services.Contracts
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepo;
        private readonly ITagRepository _tagRepo;

        public ArticleService(IMapper mapper, IArticleRepository articleRepo, ITagRepository tagRepo)
        {
            _mapper = mapper;
            _articleRepo = articleRepo;
            _tagRepo = tagRepo;
        }

        public async Task Create(CreateArticelRequest model)
        {
            var allArticleTags = _mapper.Map<List<TagForArticleRequest>>(model.Tags);

            var article = _mapper.Map<Article>(model);
            article.Tags.Clear();

            foreach (var articleTag in allArticleTags)
            {
                if (articleTag.IsSelected)
                {
                    var tag = await _tagRepo.GetById(articleTag.TagId);
                    if (tag != null)
                    {
                        article.Tags.Add(tag);
                    }
                }
            }

            await _articleRepo.Add(article);
        }

        public async Task<CreateArticelRequest> Create()
        {
            var tags = await _tagRepo.GetAllTags(); ;
            var allTags = _mapper.Map<List<TagForArticleRequest>>(tags);

            return new CreateArticelRequest
            {
                Tags = allTags
            };
        }

        public async Task<bool> Delete(Guid id)
        {
            var article = await _articleRepo.GetById(id);

            if (article != null)
            {
                await _articleRepo.Delete(article);
                return true;
            }

            return false;
        }

        public async Task<List<Article>> GetAllArticles()
        {
            return await _articleRepo.GetAllArticles();
        }

        public async Task<ArticleResponse> GetArticleById(Guid id)
        {
            // возможно стоит иклюдить
            //var article = await _context.Articles.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == id);

            var article = await _articleRepo.GetById(id);

            if (article != null)
            {
                var newArticle = _mapper.Map<ArticleResponse>(article);
                newArticle.Tags.Clear();

                var allTags = await _tagRepo.GetAllTags();

                foreach (var tag in allTags)
                {
                    var newTag = new TagForArticleRequest
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

        public async Task<bool> Update(EditArticleRequest model)
        {
            var newArticle = _mapper.Map<Article>(model);
            var article = await _articleRepo.GetById(newArticle.Id);
            //var article = await _context.Articles.Include(a => a.Tags).FirstOrDefaultAsync(a => a.Id == newArticle.Id);

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
                        var existingTag = await _tagRepo.GetById(tag.TagId);
                        if (existingTag != null)
                        {
                            article.Tags.Add(existingTag);
                        }
                    }
                }

                await _articleRepo.Update(article);
                return true;
            }

            return false;
        }

        public async Task<ArticleResponse> View(Guid id)
        {
            var article = await _articleRepo.GetArticleById(id);

            if (article == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<ArticleResponse>(article);

            return viewModel;
        }

        public async Task<bool> AddComment(CommentResponse model)
        {
            var comment = _mapper.Map<Comment>(model);

            //var article = _context.Articles.FirstOrDefault(a => a.Id == model.ArticleId);

            var article = await _articleRepo.GetById(model.ArticleId);
            if(article != null)
                comment.Article = article;

            await _articleRepo.Add(article);

            return true;
        }
    }
}
