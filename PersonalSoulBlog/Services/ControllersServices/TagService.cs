using AutoMapper;
using PersonalSoulBlog.DAL.Data;
using PersonalSoulBlog.DAL.Models.Entities;
using PersonalSoulBlog.Services.ControllersServices.Interfaces;
using PersonalSoulBlog.ViewModels.Tag;

namespace PersonalSoulBlog.Services.ControllersServices
{
    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TagService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateTag(CreateTagViewModel model)
        {
            var tag = _mapper.Map<Tag>(model);
            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();            
        }

        public async Task<bool> DeleteTag(int? id)
        {
            var tag = await _context.Tags.FindAsync(id);
            
            if(tag != null)
            {
                _context.Tags.Remove(tag);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public List<Tag> GetAllTags()
        {
            return _context.Tags.ToList();
        }

        public async Task<EditTagViewModel> GetTagById(int id)
        {
            if(id == 0)
            {
                return null;
            }

            var tag = await _context.Tags.FindAsync(id);
            if(tag != null)
            {
                var newTag = _mapper.Map<EditTagViewModel>(tag);
                return newTag;
            }

            return null;
        }

        public async Task<bool> UpdateTag(EditTagViewModel model)
        {
            var tag = await _context.Tags.FindAsync(model);

            if(tag != null) 
            {
                var newTag = _mapper.Map<Tag>(tag);

                tag.Name = newTag.Name;

                _context.Tags.Update(tag);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }
    }
}
