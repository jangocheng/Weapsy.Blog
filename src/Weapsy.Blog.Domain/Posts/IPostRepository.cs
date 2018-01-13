using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Posts
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(Guid blogId, Guid postId);
        Task<Guid> GetPostIdByTitleAsync(Guid blogId, string title);
        Task<Guid> GetPostIdBySlugAsync(Guid blogId, string slug);
        Task CreateAsync(Post post);
        Task UpdateAsync(Post post);
    }
}
