using Microsoft.AspNetCore.Mvc;

namespace Weapsy.Blog.Web.Areas.Admin.Controllers
{
    public class PostController : AdminControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
