using System.Collections.Generic;

namespace Weapsy.Blog.Domain.Posts.Events
{
    public class PostUpdated : EventBase
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Excerpt { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public PostType Type { get; set; }
        public PostStatus Status { get; set; }
    }
}
