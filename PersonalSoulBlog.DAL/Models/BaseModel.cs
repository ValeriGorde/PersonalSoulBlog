namespace PersonalSoulBlog.DAL.Models
{
    // Базовая модель
    public class BaseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
