using System;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Reporting.Blogs.Queries
{
    public class GetBlog : IQuery
    {
        public Guid BlogId { get; set; }
    }
}
