using PersonalSoulBlog.BLL.ViewModels.Comments;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.Services.Contracts.Interfaces
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
