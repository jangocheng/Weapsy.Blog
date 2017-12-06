using System;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Weapsy.Blog.Domain.Blogs;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Events;

namespace Weapsy.Blog.Domain.Tests
{
    public static class BlogFactories
    {
        public static CreateBlog CreateBlogCommand()
        {
            return new CreateBlog
            {
                AggregateRootId = Guid.NewGuid(),
                Title = "My Blog"
            };
        }

        public static BlogCreated BlogCreatedEvent()
        {
            return CreateBlogCommand().ToEvent();
        }

        public static UpdateBlog UpdateBlogCommand()
        {
            return new UpdateBlog
            {
                AggregateRootId = Guid.NewGuid(),
                Title = "My Blog"
            };
        }

        public static BlogUpdated BlogUpdatedEvent()
        {
            return UpdateBlogCommand().ToEvent();
        }

        public static Domain.Blogs.Blog Blog()
        {
            var command = CreateBlogCommand();
            var validatorMock = new Mock<IValidator<CreateBlog>>();
            validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            return new Domain.Blogs.Blog(command, validatorMock.Object);
        }
    }
}
