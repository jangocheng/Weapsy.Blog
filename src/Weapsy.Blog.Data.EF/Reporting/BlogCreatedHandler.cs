﻿using System.Threading.Tasks;
using Weapsy.Blog.Data.EF.Caching;
using Weapsy.Blog.Domain.Blogs.Events;
using Weapsy.Mediator.Events;

namespace Weapsy.Blog.Data.EF.Reporting
{
    public class BlogCreatedHandler : IEventHandlerAsync<BlogCreated>
    {
        private readonly ICacheManager _cacheManager;

        public BlogCreatedHandler(ICacheManager cacheManager)
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
