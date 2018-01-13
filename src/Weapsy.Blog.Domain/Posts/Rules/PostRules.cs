using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Posts.Rules
{
    public class PostRules : IPostRules
    {
        private readonly IPostRepository _postRepository;

        public PostRules(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> IsTitleUniqueAsync(Guid blogId, string title)
        {
            return await _postRepository.GetPostIdByTitleAsync(blogId, title) == Guid.Empty;
        }

        public async Task<bool> IsSlugUniqueAsync(Guid blogId, string slug)
        {
            return await _postRepository.GetPostIdBySlugAsync(blogId, slug) == Guid.Empty;
        }
    }
}