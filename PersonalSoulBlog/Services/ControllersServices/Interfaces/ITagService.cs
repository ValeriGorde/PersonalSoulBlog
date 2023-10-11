using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.ViewModels.Tag;

namespace PersonalSoulBlog.Services.ControllersServices.Interfaces
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
        Task<EditTagViewModel> GetTagById(int id);
        Task CreateTag(CreateTagViewModel model);
        Task<bool> UpdateTag(EditTagViewModel model);
        Task<bool> DeleteTag(int? id);
    }
}
