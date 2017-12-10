using Microsoft.AspNetCore.Mvc;

namespace Weapsy.Blog.Web.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}
