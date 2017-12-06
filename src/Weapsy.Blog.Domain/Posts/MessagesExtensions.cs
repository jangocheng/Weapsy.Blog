using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Posts
{
    public static class MessagesExtensions
    {
        public static PostCreated ToEvent(this CreatePost command)
        {
            return new PostCreated
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId,
                Title = command.Title,
                Slug = command.Slug,
                Excerpt = command.Excerpt,
                Content = command.Content,
                Tags = command.Tags,
                Status = command.Status
            };
        }

        public static PostUpdated ToEvent(this UpdatePost command)
        {
            return new PostUpdated
            {
                AggregateRootId = command.AggregateRootId,
                Title = command.Title,
                Slug = command.Slug,
                Excerpt = command.Excerpt,
                Content = command.Content,
                Tags = command.Tags,
                Status = command.Status
            };
        }
    }
}