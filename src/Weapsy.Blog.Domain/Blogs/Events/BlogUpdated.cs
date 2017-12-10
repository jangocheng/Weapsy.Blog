namespace Weapsy.Blog.Domain.Blogs.Events
{
    public class BlogUpdated : EventBase
    {
        public string Title { get; set; }
        public string Theme { get; set; }
    }
}
