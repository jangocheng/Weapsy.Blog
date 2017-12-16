using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Api
{
    public class BlogController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBlogRules _blogRules;

        public BlogController(IMediator mediator, IBlogRules blogRules) : base(mediator)
        {
            _mediator = mediator;
            _blogRules = blogRules;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBlog command)
        {
            command.AggregateRootId = Guid.NewGuid();
            await _mediator.SendAndPublishAsync<CreateBlog, Domain.Blogs.Blog>(command);
            return new NoContentResult();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBlog command)
        {
            command.AggregateRootId = BlogId;
            await _mediator.SendAndPublishAsync<UpdateBlog, Domain.Blogs.Blog>(command);
            return new NoContentResult();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetBlogSettings { BlogId = id };
            await _mediator.GetResultAsync<GetBlogSettings, BlogSettings>(query);
            return new NoContentResult();
        }

        [HttpGet]
        [Route("isBlogTitleUnique/{title}")]
        public async Task<IActionResult> IsBlogTitleUnique(string title)
        {
            var isBlogTitleUnique = await _blogRules.IsTitleUniqueAsync(title);
            return Ok(isBlogTitleUnique);
        }
    }
}
