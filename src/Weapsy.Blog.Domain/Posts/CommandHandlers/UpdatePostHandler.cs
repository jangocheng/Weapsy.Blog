using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers
{
    public class UpdatePostHandler : IDomainCommandHandlerAsync<UpdatePost>
    {
        private readonly IPostRepository _postRepository;
        private readonly IValidator<UpdatePost> _validator;

        public UpdatePostHandler(IPostRepository postRepository, IValidator<UpdatePost> validator)
        {
            _postRepository = postRepository;
            _validator = validator;           
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(UpdatePost command)
        {
            var post = await _postRepository.GetByIdAsync(command.BlogId, command.AggregateRootId);

            if (post == null)
                throw new ApplicationException("Post not found.");

            post.Update(command, _validator);

            await _postRepository.UpdateAsync(post);

            return post.Events;
        }
    }
}
