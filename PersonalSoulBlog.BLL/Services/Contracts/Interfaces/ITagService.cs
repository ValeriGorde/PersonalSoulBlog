﻿using PersonalSoulBlog.BLL.ViewModels.Tags;
using PersonalSoulBlog.DAL.Models.Entities;

namespace PersonalSoulBlog.BLL.Services.Contracts.Interfaces
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
