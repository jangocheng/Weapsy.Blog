using System.Linq;
using AutoMapper;
using Weapsy.Blog.Data.EF.Caching;
using Weapsy.Blog.Reporting.Blogs;
using Weapsy.Blog.Reporting.Blogs.Queries;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Data.EF.Reporting
{
    public class GetBlogQueryHandler : IQueryHandler<GetBlog, BlogViewModel>
    {
        private readonly IDbContextFactory _dbContextFactory;
        private readonly IMapper _mapper;
        private readonly ICacheManager _cacheManager;

        public GetBlogQueryHandler(IDbContextFactory dbContextFactory, 
            IMapper mapper, 
            ICacheManager cacheManager)
        {
            _dbContextFactory = dbContextFactory;
            _mapper = mapper;
            _cacheManager = cacheManager;
        }

        public BlogViewModel Retrieve(GetBlog query)
        {
            return _cacheManager.Get($"Blog|{query.BlogId}", 600, () =>
            {
                using (var dbContext = _dbContextFactory.CreateDbContext())
                {
                    var blogEntity = dbContext.Blogs.FirstOrDefault(x => x.Id == query.BlogId);
                    return blogEntity != null ? _mapper.Map<BlogViewModel>(blogEntity) : null;
                }
            });
        }
    }
}
