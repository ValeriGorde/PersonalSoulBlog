namespace PersonalSoulBlog.Services.Contracts.Model
{
    /// <summary>
    /// Модель вывода результата при регистрации
    /// </summary>
    public class RegisterResult
    {
        public bool Success { get; set; }   
        public List<string> Errors { get; set; }   
    }
}
