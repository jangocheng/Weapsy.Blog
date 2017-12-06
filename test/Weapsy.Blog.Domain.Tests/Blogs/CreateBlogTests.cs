using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Blogs;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Events;

namespace Weapsy.Blog.Domain.Tests.Blogs
{
    [TestFixture]
    public class CreateBlogTests
    {
        private CreateBlog _command;
        private Mock<IValidator<CreateBlog>> _validatorMock;
        private Domain.Blogs.Blog _blog;
        private BlogCreated _event;

        [SetUp]
        public void Setup()
        {
            _command = BlogFactories.CreateBlogCommand();
            _validatorMock = new Mock<IValidator<CreateBlog>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());
            _blog = new Domain.Blogs.Blog(_command, _validatorMock.Object);
            _event = _command.ToEvent();
        }

        [Test]
        public void ValidatesCommand()
        {
            _validatorMock.Verify(x => x.Validate(_command), Times.Once);
        }

        [Test]
        public void SetsId()
        {
            Assert.AreEqual(_event.AggregateRootId, _blog.Id);
        }

        [Test]
        public void SetsTitle()
        {
            Assert.AreEqual(_event.Title, _blog.Title);
        }
    }
}
