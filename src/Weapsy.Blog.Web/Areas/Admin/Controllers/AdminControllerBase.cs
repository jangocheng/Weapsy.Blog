using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Weapsy.Blog.Web.Configuration;

namespace Weapsy.Blog.Web.Areas.Admin.Controllers
{    
    [Area("Admin")]
    [Authorize(Roles = Constants.AdministratorRoleName)]
    public abstract class AdminControllerBase : Controller
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