using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Tags;

namespace PersonalSoulBlog.Services.Contracts.Interfaces
{
    public interface ITagService
    {
        Task<List<Tag>> GetAllTags();
        Task<EditTagRequest> GetTagById(Guid id);
        Task CreateTag(CreateTagRequest model);
        Task<bool> UpdateTag(EditTagRequest model);
        Task<bool> DeleteTag(Guid id);
    }
}
