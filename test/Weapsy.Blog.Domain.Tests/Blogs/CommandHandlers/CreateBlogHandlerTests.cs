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
    public class CreateBlogHandlerTests
    {
        private CreateBlog _command;
        private BlogCreated _event;
        private Domain.Blogs.Blog _blog;
        private IEnumerable<IDomainEvent> _result;

        private Mock<IBlogRepository> _blogRepositoryMock;
        private Mock<IValidator<CreateBlog>> _validatorMock;
        private IDomainCommandHandlerAsync<CreateBlog> _commandHandler;

        [SetUp]
        public async Task Setup()
        {
            _command = BlogFactories.CreateBlogCommand();

            _validatorMock = new Mock<IValidator<CreateBlog>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());

            _blogRepositoryMock = new Mock<IBlogRepository>();
            _blogRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Domain.Blogs.Blog>()))
                .Callback<Domain.Blogs.Blog>(b => _blog = b)
                .Returns(Task.CompletedTask);

            _commandHandler = new CreateBlogHandler(_blogRepositoryMock.Object, _validatorMock.Object);
            _result = await _commandHandler.HandleAsync(_command);

            _event = _blog.Events.OfType<BlogCreated>().Single();
        }

        [Test]
        public void SavesBlog()
        {
            _blogRepositoryMock.Verify(x => x.CreateAsync(_blog), Times.Once);
        }

        [Test]
        public void ReturnsEvents()
        {
            Assert.AreEqual(_event, _result.Single());
        }
    }
}
