using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers
{
    public class DeletePostHandler : IDomainCommandHandlerAsync<DeletePost>
    {
        private readonly IPostRepository _postRepository;

        public DeletePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;       
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(DeletePost command)
        {
            var post = await _postRepository.GetByIdAsync(command.BlogId, command.AggregateRootId);

            if (post == null)
                throw new ApplicationException("Post not found.");

            post.Delete();

            await _postRepository.UpdateAsync(post);

            return post.Events;
        }
    }
}
