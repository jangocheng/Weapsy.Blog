using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Posts.Converters
{
    public class CommandConverter : ICommandConverter
    {
        public PostCreated ToEvent(CreatePost command)
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

        public PostUpdated ToEvent(UpdatePost command)
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

        public PostPublished ToEvent(PublishPost command)
        {
            return new PostPublished
            {
                AggregateRootId = command.AggregateRootId,
                BlogId = command.BlogId
            };
        }
    }
}