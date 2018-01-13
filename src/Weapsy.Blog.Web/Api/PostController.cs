using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Rules;
using Weapsy.Blog.Reporting.Models;
using Weapsy.Blog.Reporting.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Api
{
    public class PostController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPostRules _postRules;

        public PostController(IMediator mediator, IPostRules postRules)
        {
            _mediator = mediator;
            _postRules = postRules;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreatePost command)
        {
            command.AggregateRootId = Guid.NewGuid();
            command.BlogId = BlogId;
            await _mediator.SendAndPublishAsync<CreatePost, Post>(command);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePost command)
        {
            command.BlogId = BlogId;
            await _mediator.SendAndPublishAsync<UpdatePost, Post>(command);
            return NoContent();
        }

        [HttpGet]
        [AllowAnonymous]
        public Task<IActionResult> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{slug}")]
        [AllowAnonymous]
        public Task<IActionResult> Get(string slug)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{slug}/viewModel")]
        [AllowAnonymous]
        public async Task<IActionResult> GetViewModel(string slug)
        {
            var query = new GetPostViewModel { BlogId = BlogId, PostSlug = slug };
            var viewModel = await _mediator.GetResultAsync<GetPostViewModel, PostViewModel>(query);
            if (viewModel == null)
            {
                return NotFound();
            }
            return Ok(viewModel);
        }

        [HttpPut]
        [Route("{id}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            var command = new PublishPost { BlogId = BlogId, AggregateRootId = id };
            await _mediator.SendAndPublishAsync<PublishPost, Post>(command);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePost{ BlogId = BlogId, AggregateRootId = id };
            await _mediator.SendAndPublishAsync<DeletePost, Post>(command);
            return NoContent();
        }

        [HttpPut]
        [Route("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var command = new RestorePost { BlogId = BlogId, AggregateRootId = id };
            await _mediator.SendAndPublishAsync<RestorePost, Post>(command);
            return NoContent();
        }

        [HttpGet]
        [Route("isPostTitleUnique/{title}")]
        public async Task<IActionResult> IsPostTitleUnique(string title)
        {
            var isPostTitleUnique = await _postRules.IsTitleUniqueAsync(BlogId, title);
            return Ok(isPostTitleUnique);
        }

        [HttpGet]
        [Route("isPostSlugUnique/{slug}")]
        public async Task<IActionResult> IsPostSlugUnique(string slug)
        {
            var isPostSlugUnique = await _postRules.IsSlugUniqueAsync(BlogId, slug);
            return Ok(isPostSlugUnique);
        }
    }
}
