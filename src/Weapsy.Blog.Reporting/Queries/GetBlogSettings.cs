using System;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Reporting.Queries
{
    public class GetBlogSettings : IQuery
    {
        public Guid BlogId { get; set; }
    }
}
