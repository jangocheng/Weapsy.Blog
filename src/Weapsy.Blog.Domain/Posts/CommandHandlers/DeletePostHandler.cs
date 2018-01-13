using System;
using System.Threading.Tasks;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers
{
    public class DeletePostHandler : IDomainCommandHandlerAsync<DeletePost>
    {
        private readonly IPostRepository _postRepository;
        private readonly IDeletePostValidator _validator;

        public DeletePostHandler(IPostRepository postRepository, IDeletePostValidator validator)
        {
            _postRepository = postRepository;
            _validator = validator;
        }

        public async Task<IAggregateRoot> HandleAsync(DeletePost command)
        {
            var post = await _postRepository.GetByIdAsync(command.BlogId, command.AggregateRootId);

            if (post == null)
                throw new ApplicationException("Post not found.");

            post.Delete(command, _validator);

            await _postRepository.UpdateAsync(post);

            return post;
        }
    }
}
