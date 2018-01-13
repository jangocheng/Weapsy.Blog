using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Api
{
    public class BlogController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBlogRules _blogRules;

        public BlogController(IMediator mediator, IBlogRules blogRules)
        {
            _mediator = mediator;
            _blogRules = blogRules;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBlog command)
        {
            command.AggregateRootId = Guid.NewGuid();
            await _mediator.SendAndPublishAsync<CreateBlog, Domain.Blogs.Blog>(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateBlog command)
        {
            command.AggregateRootId = BlogId;
            await _mediator.SendAndPublishAsync<UpdateBlog, Domain.Blogs.Blog>(command);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<IActionResult> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}/indexViewModel")]
        [AllowAnonymous]
        public Task<IActionResult> GetIndexViewModel(Guid id)
        {
            throw new NotImplementedException();
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
