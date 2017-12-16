using System.Linq;
using AutoMapper;
using Weapsy.Blog.Data.Caching;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Data.Reporting
{
    public class GetBlogSettingsQueryHandler : IQueryHandler<GetBlogSettings, BlogSettings>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cacheManager;

        public GetBlogSettingsQueryHandler(IDbContextFactory dbContextFactory, 
            IMapper mapper, 
            ICacheManager cacheManager)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public BlogSettings Retrieve(GetBlogSettings query)
        {
            return _cacheManager.Get($"Blog|{query.BlogId}", 600, () =>
            {
                using (var dbContext = _dbContextFactory.CreateDbContext())
                {
                    var blogEntity = dbContext.Blogs.FirstOrDefault(x => x.Id == query.BlogId);
                    return blogEntity != null ? _mapper.Map<BlogSettings>(blogEntity) : null;
                }
            });
        }
    }
}
