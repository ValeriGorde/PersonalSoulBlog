using AutoMapper;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Comments;
using PersonalSoulBlog.ViewModels.Roles;
using PersonalSoulBlog.ViewModels.Tags;

namespace PersonalSoulBlog.Services.Contracts
{
    /// <summary>
    /// Сервис по обработке crud методов для контроллера Comment
    /// </summary>
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task CreateComment(CreateCommentRequest model)
        {
            var comment = _mapper.Map<Comment>(model);
            await _commentRepo.Add(comment);
        }

        public async Task<Guid> DeleteComment(Guid id)
        {
            var comment = await _commentRepo.GetById(id);
            if (comment != null)
            {
                await _commentRepo.Delete(comment);
                return comment.ArticleId;
            }
            return Guid.Empty;
        }

        public async Task<Guid> UpdateComment(EditCommentRequest model)
        {
            var comment = await _commentRepo.GetById(model.Id);

            if (comment != null)
            {
                comment.Text = model.Text;
                await _commentRepo.Update(comment);
                return comment.ArticleId;
            }

            return Guid.Empty;
        }

        public async Task<List<Comment>> GetAllComments()
        {
            return await _commentRepo.GetAllComments();
        }

        public async Task<EditCommentRequest> GetCommentById(Guid id)
        {
            var comment = await _commentRepo.GetById(id);

            if (comment != null)
            {
                var newComment = _mapper.Map<EditCommentRequest>(comment);
                return newComment;
            }

            //логирование сюда
            return null;
        }
    }
}
