using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Posts.Rules
{
    public interface IPostRules
    {
        Task<bool> IsTitleUniqueAsync(Guid blogId, Guid postId, string title);
        Task<bool> IsSlugUniqueAsync(Guid blogId, Guid postId, string slug);
    }
}
