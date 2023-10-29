namespace PersonalSoulBlog.BLL.ViewModels.Comments
{
    /// <summary>
    /// Запрос для редактироваия комментария
    /// </summary>
    public class EditCommentRequest
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid ArticleId { get; set; }
    }
}
