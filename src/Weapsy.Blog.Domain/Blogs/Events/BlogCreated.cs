namespace Weapsy.Blog.Domain.Blogs.Events
{
    public class BlogCreated : EventBase
    {
        public string Title { get; set; }
        public string Theme { get; set; }
    }
}
