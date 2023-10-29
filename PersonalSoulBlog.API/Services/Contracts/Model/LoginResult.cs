namespace PersonalSoulBlog.Services.Contracts.Model
{
    /// <summary>
    /// Модель результата при авторизации
    /// </summary>
    public class LoginResult
    {
        public bool Success { get; set; }
        public string RedirectUrl { get; set; }
        public string ErrorMessage { get; set; }
    }
}
