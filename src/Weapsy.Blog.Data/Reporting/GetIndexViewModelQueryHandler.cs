using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Weapsy.Blog.Data.Caching;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Data.Reporting
{
    public class GetIndexViewModelQueryHandler : IQueryHandlerAsync<GetIndexViewModel, IndexViewModel>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly ICacheManager _cacheManager;

        public GetIndexViewModelQueryHandler(IDbContextFactory dbContextFactory, ICacheManager cacheManager)
        {
            _dbContextFactory = dbContextFactory;
            _cacheManager = cacheManager;
        }

        public async Task<IndexViewModel> RetrieveAsync(GetIndexViewModel query)
        {
            return await _cacheManager.GetAsync($"Index|{query.BlogId}", 600, async () =>
            {
                using (var dbContext = _dbContextFactory.CreateDbContext())
                {
                    var blog = await dbContext.Blogs.FirstOrDefaultAsync(b => b.Id == query.BlogId);

                    var posts = await dbContext.Entry(blog)
                        .Collection(b => b.Posts)
                        .Query()
                        .Where(p => p.Status == PostStatus.Published)
                        .OrderByDescending(p => p.StatusTimeStamp)
                        .ToListAsync();

                    return new IndexViewModel
                    {
                        BlogId = blog.Id,
                        BlogTitle = blog.Title,
                        BlogTheme = blog.Theme,
                        Posts = posts.Select(p => new IndexPostViewModel
                        {
                            Id = p.Id,
                            Title = p.Title,
                            Summary = p.Excerpt
                        }).ToList()
                    };
                }
            });
        }
    }
}
