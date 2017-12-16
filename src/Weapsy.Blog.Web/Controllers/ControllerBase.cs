using System;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        private static IMediator _mediator;

        protected ControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }

        //protected static BlogSettings Blog
        //{
        //    get
        //    {
        //        var query = new GetBlogSettings { BlogId = Constants.DefaultBlogId };
        //        return _mediator.GetResult<GetBlogSettings, BlogSettings>(query);
        //    }
        //}

        protected Guid BlogId { get; } = Constants.DefaultBlogId;
    }
}