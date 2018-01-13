using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Web.Configuration;

namespace Weapsy.Blog.Web.Controllers
{
    public abstract class ControllerBase : Controller
    {
        protected Guid BlogId
        {
            get
            {
                var options = HttpContext.RequestServices.GetService<IOptions<BlogSettings>>();
                return Guid.Parse(options.Value.DefaultBlogId);
            }
        }
    }
}