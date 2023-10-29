using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Comments;
using PersonalSoulBlog.ViewModels.Roles;

namespace PersonalSoulBlog.Services.Contracts.Interfaces
{
    public interface ICommentService
    {
        Task CreateComment(CreateCommentRequest model);
        Task<Guid> DeleteComment(Guid id);
        Task<Guid> UpdateComment(EditCommentRequest model);
        Task<EditCommentRequest> GetCommentById(Guid id);
        Task<List<Comment>> GetAllComments();
    }
}
