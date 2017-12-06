using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Blogs.CommandHandlers
{
    public class CreateBlogHandler : IDomainCommandHandlerAsync<CreateBlog>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IValidator<CreateBlog> _validator;       

        public CreateBlogHandler(IBlogRepository blogRepository, IValidator<CreateBlog> validator)
        {
            _blogRepository = blogRepository;
            _validator = validator;           
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(CreateBlog command)
        {
            var blog = new Blog(command, _validator);

            await _blogRepository.CreateAsync(blog);

            return blog.Events;
        }
    }
}
