namespace PersonalSoulBlog.ViewModels.Articles
{
    public class TagForArticleRequest
    {
        public Guid TagId { get; set; }
        public string? TagName { get; set; }
        public bool IsSelected { get;set; }
    }
}
