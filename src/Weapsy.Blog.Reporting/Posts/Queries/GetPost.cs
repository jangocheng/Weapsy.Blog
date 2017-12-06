using System;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Reporting.Posts.Queries
{
    public class GetPost : IQuery
    {
        public Guid BlogId { get; set; }
        public Guid PostId { get; set; }
    }
}
