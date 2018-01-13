using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Posts.Rules
{
    public interface IPostRules
    {
        Task<bool> IsTitleUniqueAsync(Guid blogId, string title);
        Task<bool> IsSlugUniqueAsync(Guid blogId, string slug);
    }
}
