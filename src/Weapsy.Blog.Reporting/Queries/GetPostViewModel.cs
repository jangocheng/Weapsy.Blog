using System;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Reporting.Queries
{
    public class GetPostViewModel : IQuery
    {
        public Guid BlogId { get; set; }
        public string PostSlug { get; set; }
    }
}
