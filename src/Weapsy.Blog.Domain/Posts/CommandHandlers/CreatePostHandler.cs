using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers
{
    public class CreatePostHandler : IDomainCommandHandlerAsync<CreatePost>
    {
        private readonly IPostRepository _postRepository;
        private readonly IValidator<CreatePost> _validator;

        public CreatePostHandler(IPostRepository postRepository, IValidator<CreatePost> validator)
        {
            _postRepository = postRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(CreatePost command)
        {
            var post = new Post(command, _validator);

            await _postRepository.CreateAsync(post);

            return post.Events;
        }
    }
}
