using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Web.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Weapsy.Blog.Web.Api
{    
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = Constants.AdministratorRoleName)]
    public abstract class ApiControllerBase : Controller
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