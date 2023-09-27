using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalSoulBlog.Models.Entities
{
    // Модель тега
    public class Tag : BaseModel
    {
        public string Name { get; set; }

        [InverseProperty("Tags")]
        public List<Article> Articles { get; set; } = new List<Article>();
    }
}
