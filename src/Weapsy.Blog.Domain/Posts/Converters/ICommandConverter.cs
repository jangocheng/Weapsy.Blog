using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Posts.Converters
{
    public interface ICommandConverter
    {
        PostCreated ToEvent(CreatePost command);
        PostUpdated ToEvent(UpdatePost command);
        PostPublished ToEvent(PublishPost command);
    }
}
