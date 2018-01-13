using System.Threading.Tasks;
using Weapsy.Blog.Data.Caching;
using Weapsy.Blog.Domain.Posts.Events;
using Weapsy.Mediator.Events;

namespace Weapsy.Blog.Data.Reporting.EventHandlers
{
    public class PostUpdatedEventHandler : IEventHandlerAsync<PostUpdated>
    {
        private readonly ICacheManager _cacheManager;

        public PostUpdatedEventHandler(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task HandleAsync(PostUpdated @event)
        {
            await Task.CompletedTask;

            _cacheManager.Remove(CacheKeys.PostCacheKey(@event.BlogId, @event.Slug));
        }
    }
}
