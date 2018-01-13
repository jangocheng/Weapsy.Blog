using System.Threading.Tasks;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers
{
    public class CreatePostHandler : IDomainCommandHandlerAsync<CreatePost>
    {
        private readonly IPostRepository _postRepository;
        private readonly ICreatePostValidator _validator;

        public CreatePostHandler(IPostRepository postRepository, ICreatePostValidator validator)
        {
            _postRepository = postRepository;
            _validator = validator;
        }

        public async Task<IAggregateRoot> HandleAsync(CreatePost command)
        {
            var post = new Post(command, _validator);

            await _postRepository.CreateAsync(post);

            return post;
        }
    }
}
