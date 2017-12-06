using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Rules;
using Weapsy.Blog.Reporting.Posts;
using Weapsy.Blog.Reporting.Posts.Queries;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Api
{
    public class PostController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IPostRules _postRules;

        public PostController(IMediator mediator, IPostRules postRules) : base(mediator)
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
            return new NoContentResult();
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdatePost command)
        {
            command.BlogId = BlogId;
            await _mediator.SendAndPublishAsync<UpdatePost, Post>(command);
            return new NoContentResult();
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Get(Guid id)
        {
            var query = new GetPost{ BlogId = BlogId, PostId = id };
            await _mediator.GetResultAsync<GetPost, PostViewModel>(query);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/publish")]
        public async Task<IActionResult> Publish(Guid id)
        {
            var command = new PublishPost { BlogId = BlogId, AggregateRootId = id };
            await _mediator.SendAndPublishAsync<PublishPost, Post>(command);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePost{ BlogId = BlogId, AggregateRootId = id };
            await _mediator.SendAndPublishAsync<DeletePost, Post>(command);
            return new NoContentResult();
        }

        [HttpPut]
        [Route("{id}/restore")]
        public async Task<IActionResult> Restore(Guid id)
        {
            var command = new RestorePost { BlogId = BlogId, AggregateRootId = id };
            await _mediator.SendAndPublishAsync<RestorePost, Post>(command);
            return new NoContentResult();
        }

        [HttpGet]
        [Route("{id}/isPostTitleUnique/{title}")]
        public async Task<IActionResult> IsPostTitleUnique(Guid id, string title)
        {
            var isPostTitleUnique = await _postRules.IsTitleUniqueAsync(BlogId, id, title);
            return Ok(isPostTitleUnique);
        }

        [HttpGet]
        [Route("{id}/isPostSlugUnique/{slug}")]
        public async Task<IActionResult> IsPostSlugUnique(Guid id, string slug)
        {
            var isPostSlugUnique = await _postRules.IsSlugUniqueAsync(BlogId, id, slug);
            return Ok(isPostSlugUnique);
        }
    }
}
