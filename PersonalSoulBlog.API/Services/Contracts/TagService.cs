using AutoMapper;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.DAL.Models.Repositories.Interfaces;
using PersonalSoulBlog.Services.Contracts.Interfaces;
using PersonalSoulBlog.ViewModels.Tags;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class TagService: ITagService
    {
        private readonly ITagRepository _tagRepo;
        private readonly IMapper _mapper;

        public TagService(IMapper mapper, ITagRepository tagRepo)
        {
            _tagRepo = tagRepo;
            _mapper = mapper;
        }

        public async Task CreateTag(CreateTagRequest model)
        {
            var tag = _mapper.Map<Tag>(model);
            await _tagRepo.Add(tag);        
        }

        public async Task<bool> DeleteTag(Guid id)
        {
            var tag = await _tagRepo.GetById(id);
            
            if(tag != null)
            {
                await _tagRepo.Delete(tag);

                return true;
            }

            return false;
        }

        public async Task<List<Tag>> GetAllTags()
        {
            return await _tagRepo.GetAll();
        }

        public async Task<EditTagRequest> GetTagById(Guid id)
        {
            var tag = await _tagRepo.GetById(id);

            if(tag != null)
            {
                var newTag = _mapper.Map<EditTagRequest>(tag);
                return newTag;
            }

            //логирование сюда
            return null;
        }

        public async Task<bool> UpdateTag(EditTagRequest model)
        {
            var tag = await _tagRepo.GetById(model.Id);

            if(tag != null) 
            {
                tag.Name = model.Name;
                await _tagRepo.Update(tag);
                return true;
            }

            return false;
        }
    }
}
