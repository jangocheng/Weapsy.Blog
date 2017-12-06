using System.Threading.Tasks;
using Weapsy.Blog.Domain.Posts.Events;
using Weapsy.Mediator.Events;

namespace Weapsy.Blog.Reporting.Posts.EventHandlers
{
    public class PostCreateddHandler : IEventHandlerAsync<PostCreated>
    {
        public async Task HandleAsync(PostCreated @event)
        {
            await Task.CompletedTask;
        }
    }
}
