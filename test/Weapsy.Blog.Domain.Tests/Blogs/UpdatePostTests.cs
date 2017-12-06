using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Events;

namespace Weapsy.Blog.Domain.Tests.Blogs
{
    [TestFixture]
    public class UpdateBlogTests
    {
        private UpdateBlog _command;
        private Mock<IValidator<UpdateBlog>> _validatorMock;
        private Domain.Blogs.Blog _blog;
        private BlogUpdated _event;

        [SetUp]
        public void Setup()
        {
            _blog = BlogFactories.Blog();
            _command = BlogFactories.UpdateBlogCommand();
            _validatorMock = new Mock<IValidator<UpdateBlog>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());
            _blog.Update(_command, _validatorMock.Object);
            _event = _blog.Events.OfType<BlogUpdated>().Single();
        }

        [Test]
        public void ValidatesCommand()
        {
            _validatorMock.Verify(x => x.Validate(_command), Times.Once);
        }

        [Test]
        public void SetsTitle()
        {
            Assert.AreEqual(_event.Title, _blog.Title);
        }
    }
}
