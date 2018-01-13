using System;
using System.Threading.Tasks;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers
{
    public class RestorePostHandler : IDomainCommandHandlerAsync<RestorePost>
    {
        private readonly IPostRepository _postRepository;
        private readonly IRestorePostValidator _validator;

        public RestorePostHandler(IPostRepository postRepository, IRestorePostValidator validator)
        {
            _postRepository = postRepository;
            _validator = validator;
        }

        public async Task<IAggregateRoot> HandleAsync(RestorePost command)
        {
            var post = await _postRepository.GetByIdAsync(command.BlogId, command.AggregateRootId);

            if (post == null)
                throw new ApplicationException("Post not found.");

            post.Restore(command, _validator);

            await _postRepository.UpdateAsync(post);

            return post;
        }
    }
}
