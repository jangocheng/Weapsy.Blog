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

        public async Task<bool> IsTitleUniqueAsync(Guid blogId, Guid postId, string title)
        {
            return await _postRepository.GetPostIdByTitleAsync(blogId, postId, title) == Guid.Empty;
        }

        public async Task<bool> IsSlugUniqueAsync(Guid blogId, Guid postId, string slug)
        {
            return await _postRepository.GetPostIdBySlugAsync(blogId, postId, slug) == Guid.Empty;
        }
    }
}