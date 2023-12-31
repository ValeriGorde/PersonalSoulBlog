﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonalSoulBlog.BLL.Services.Contracts.Interfaces;
using PersonalSoulBlog.BLL.ViewModels.Articles;
using PersonalSoulBlog.BLL.ViewModels.Comments;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;

namespace PersonalSoulBlog.BLL.Services.Contracts
{
    public class ArticleService : IArticleService
    {
        private readonly IMapper _mapper;
        private readonly IArticleRepository _articleRepo;
        private readonly ITagRepository _tagRepo;
        private readonly ICommentRepository _commentRepo;

        public ArticleService(IMapper mapper, IArticleRepository articleRepo, 
            ITagRepository tagRepo, ICommentRepository commentRepo)
        {
            _mapper = mapper;
            _articleRepo = articleRepo;
            _tagRepo = tagRepo;
            _commentRepo = commentRepo;
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

        public async Task<ArticleView> GetArticleById(Guid id)
        {           
            var article = await _articleRepo.GetArticleById(id);

            if (article != null)
            {
                var newArticle = _mapper.Map<ArticleView>(article);
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
            var article = await _articleRepo.GetArticleById(newArticle.Id);

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

        public async Task<ArticleView> View(Guid id)
        {
            var article = await _articleRepo.GetArticleById(id);

            if (article == null)
            {
                return null;
            }

            var viewModel = _mapper.Map<ArticleView>(article);

            return viewModel;
        }

        public async Task<bool> AddComment(CommentView model)
        {
            var comment = _mapper.Map<Comment>(model);

            // находим статью и привязываем к комментарию
            var article = await _articleRepo.GetArticleById(model.ArticleId);
            if(article != null)
                comment.Article = article;

            await _commentRepo.Add(comment);

            return true;
        }
    }
}
