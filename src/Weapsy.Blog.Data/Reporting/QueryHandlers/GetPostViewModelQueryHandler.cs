using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weapsy.Blog.Data.Caching;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Data.Reporting.QueryHandlers
{
    public class GetPostViewModelQueryHandler : IQueryHandlerAsync<GetPostViewModel, PostViewModel>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ICacheManager _cacheManager;

        public GetPostViewModelQueryHandler(IDbContextFactory dbContextFactory, ICacheManager cacheManager)
        {
            _dbContextFactory = dbContextFactory;
            _cacheManager = cacheManager;
        }

        public async Task<PostViewModel> RetrieveAsync(GetPostViewModel query)
        {
            return await _cacheManager.GetAsync(CacheKeys.PostCacheKey(query.BlogId, query.PostSlug), 600, async () =>
            {
                using (var dbContext = _dbContextFactory.CreateDbContext())
                {
                    return await dbContext.Posts
                        .Where(p => p.BlogId == query.BlogId && p.Slug == query.PostSlug)
                        .Select(p => new PostViewModel
                        {
                            PostId = p.Id,
                            PostTitle = p.Title,
                            PostContent = p.Content,
                            PostPublishedDate = p.StatusTimeStamp
                        })
                        .FirstOrDefaultAsync();
                }
            });
        }
    }
}
