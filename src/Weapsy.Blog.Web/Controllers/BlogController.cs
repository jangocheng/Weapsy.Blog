﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Controllers
{
    public class BlogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BlogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetIndexViewModel{ BlogId = BlogId };
            var viewModel = await _mediator.GetResultAsync<GetIndexViewModel, IndexViewModel>(query);
            return View(viewModel);
        }

        [Route("blog/{slug}")]
        public async Task<IActionResult> Post(string slug)
        {
            var query = new GetPostViewModel { BlogId = BlogId, PostSlug = slug };
            var viewModel = await _mediator.GetResultAsync<GetPostViewModel, PostViewModel>(query);
            if (viewModel == null)
            {
                return NotFound();
            }
            return View(viewModel);
        }
    }
}
