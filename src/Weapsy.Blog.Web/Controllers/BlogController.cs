using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Controllers
{
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator) : base(mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetIndexViewModel{ BlogId = BlogId };
            var viewModel = await _mediator.GetResultAsync<GetIndexViewModel, IndexViewModel>(query);
            return View(viewModel);
        }

        public IActionResult Post()
        {
            return View();
        }
    }
}
