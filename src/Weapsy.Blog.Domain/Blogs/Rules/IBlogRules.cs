using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Blogs.Rules
{
    public interface IBlogRules
    {
        Task<bool> DoesBlogExistAsync(Guid blogId);
        Task<bool> IsTitleUniqueAsync(string title);        
    }
}
