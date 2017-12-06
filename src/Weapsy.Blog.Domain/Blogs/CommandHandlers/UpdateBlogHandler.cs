using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Blogs.CommandHandlers
{
    public class UpdateBlogHandler : IDomainCommandHandlerAsync<UpdateBlog>
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IValidator<UpdateBlog> _validator;       

        public UpdateBlogHandler(IBlogRepository blogRepository, IValidator<UpdateBlog> validator)
        {
            _blogRepository = blogRepository;
            _validator = validator;           
        }

        public async Task<IEnumerable<IDomainEvent>> HandleAsync(UpdateBlog command)
        {
            var blog = await _blogRepository.GetByIdAsync(command.AggregateRootId);

            if (blog == null)
                throw new ApplicationException("Blog not found.");

            blog.Update(command, _validator);

            await _blogRepository.UpdateAsync(blog);

            return blog.Events;
        }
    }
}
