using System.Threading.Tasks;
using Weapsy.Blog.Data.Caching;
using Weapsy.Blog.Domain.Blogs.Events;
using Weapsy.Mediator.Events;

namespace Weapsy.Blog.Data.Reporting
{
    public class BlogCreatedEventHandler : IEventHandlerAsync<BlogCreated>
    {
        private readonly ICacheManager _cacheManager;

        public BlogCreatedEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task HandleAsync(BlogCreated @event)
        {
            await Task.CompletedTask;

            _cacheManager.Remove($"Blog|{@event.AggregateRootId}");
        }
    }
}
