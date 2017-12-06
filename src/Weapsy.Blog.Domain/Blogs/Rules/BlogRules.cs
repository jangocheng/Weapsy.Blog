using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Blogs.Rules
{
    public class BlogRules : IBlogRules
    {
        private readonly IBlogRepository _blogRepository;

        public BlogRules(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        public async Task<bool> DoesBlogExistAsync(Guid blogId)
        {
            return await _blogRepository.GetByIdAsync(blogId) != null;
        }

        public async Task<bool> IsTitleUniqueAsync(string title)
        {
            return await _blogRepository.GetBlogIdByTitleAsync(title) == Guid.Empty;
        }
    }
}