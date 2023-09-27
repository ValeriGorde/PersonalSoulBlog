using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSoulBlog.Models.Entities
{
    // Модель статьи
    public class Article : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }

        [InverseProperty("Articles")]
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }
}
