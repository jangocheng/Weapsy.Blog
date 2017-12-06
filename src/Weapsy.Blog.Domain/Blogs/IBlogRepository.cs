using System;
using System.Threading.Tasks;

namespace Weapsy.Blog.Domain.Blogs
{
    public interface IBlogRepository
    {
        Task<Blog> GetByIdAsync(Guid blogId);
        Task<Guid> GetBlogIdByTitleAsync(string title);        
        Task CreateAsync(Blog blog);
        Task UpdateAsync(Blog blog);
    }
}
