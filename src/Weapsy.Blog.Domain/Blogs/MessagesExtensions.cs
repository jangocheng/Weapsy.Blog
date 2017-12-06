using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Events;

namespace Weapsy.Blog.Domain.Blogs
{
    public static class MessagesExtensions
    {
        public static BlogCreated ToEvent(this CreateBlog command)
        {
            return new BlogCreated
            {
                AggregateRootId = command.AggregateRootId,
                Title = command.Title
            };
        }

        public static BlogUpdated ToEvent(this UpdateBlog command)
        {
            return new BlogUpdated
            {
                AggregateRootId = command.AggregateRootId,
                Title = command.Title
            };
        }
    }
}