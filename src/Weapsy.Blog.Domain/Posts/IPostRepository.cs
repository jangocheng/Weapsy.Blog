using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Posts
{
    public interface IPostRepository
    {
        Task<Post> GetByIdAsync(Guid blogId, Guid postId);
        Task<Guid> GetPostIdByTitleAsync(Guid blogId, Guid postId, string title);
        Task<Guid> GetPostIdBySlugAsync(Guid blogId, Guid postId, string slug);
        Task CreateAsync(Post post);
        Task UpdateAsync(Post post);
    }
}
