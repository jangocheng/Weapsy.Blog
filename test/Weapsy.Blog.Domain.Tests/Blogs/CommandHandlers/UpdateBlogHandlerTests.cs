using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Blogs;
using Weapsy.Blog.Domain.Blogs.CommandHandlers;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Events;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Tests.Blogs.CommandHandlers
{
    [TestFixture]
    public class UpdateBlogHandlerTests
    {
        private UpdateBlog _command;
        private BlogUpdated _event;
        private Domain.Blogs.Blog _blog;
        private Domain.Blogs.Blog _updatedBlog;
        private IEnumerable<IDomainEvent> _result;

        private Mock<IBlogRepository> _blogRepositoryMock;
        private Mock<IValidator<UpdateBlog>> _validatorMock;
        private IDomainCommandHandlerAsync<UpdateBlog> _commandHandler;

        [SetUp]
        public async Task Setup()
        {
            _blog = BlogFactories.Blog();

            _command = BlogFactories.UpdateBlogCommand();

            _validatorMock = new Mock<IValidator<UpdateBlog>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());

            _blogRepositoryMock = new Mock<IBlogRepository>();
            _blogRepositoryMock
                .Setup(x => x.GetByIdAsync(_command.AggregateRootId))
                .ReturnsAsync(_blog);
            _blogRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Domain.Blogs.Blog>()))
                .Callback<Domain.Blogs.Blog>(b => _updatedBlog = b)
                .Returns(Task.CompletedTask);

            _commandHandler = new UpdateBlogHandler(_blogRepositoryMock.Object, _validatorMock.Object);
            _result = await _commandHandler.HandleAsync(_command);

            _event = _updatedBlog.Events.OfType<BlogUpdated>().Single();
        }

        [Test]
        public void ThrowsExceptionWhenPostIsNotFound()
        {
            _blogRepositoryMock
                .Setup(x => x.GetByIdAsync(_command.AggregateRootId))
                .ReturnsAsync((Domain.Blogs.Blog)null);

            Assert.ThrowsAsync<ApplicationException>(async () => await _commandHandler.HandleAsync(_command));
        }

        [Test]
        public void ReturnsEvents()
        {
            Assert.AreEqual(_event, _result.OfType<BlogUpdated>().Single());
        }
    }
}
