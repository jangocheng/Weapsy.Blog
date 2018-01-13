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
                Type = command.Type,
                Status = command.Status
            };
        }

        public static PostUpdated ToEvent(this UpdatePost command)
        {
            return new PostUpdated
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId,
                Title = command.Title,
                Slug = command.Slug,
                Excerpt = command.Excerpt,
                Content = command.Content,
                Tags = command.Tags,
                Type = command.Type,
                Status = command.Status
            };
        }

        public static PostPublished ToEvent(this PublishPost command)
        {
            return new PostPublished
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId
            };
        }

        public static PostWithdrew ToEvent(this WithdrawPost command)
        {
            return new PostWithdrew
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId
            };
        }

        public static PostDeleted ToEvent(this DeletePost command)
        {
            return new PostDeleted
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId
            };
        }

        public static PostRestored ToEvent(this RestorePost command)
        {
            return new PostRestored
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId
            };
        }
    }
}