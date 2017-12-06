using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Reporting.Blogs;
using Weapsy.Blog.Reporting.Blogs.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Api
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ControllerBase : Controller
    {
        private static IMediator _mediator;

        protected ControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        protected static BlogViewModel Blog
        {
            get
            {
                var blogId = new Guid(Constants.DefaultBlogId);
                var query = new GetBlog { BlogId = blogId };
                return _mediator.GetResult<GetBlog, BlogViewModel>(query);
            }
        }

        protected Guid BlogId { get; } = Blog.Id;
    }
}