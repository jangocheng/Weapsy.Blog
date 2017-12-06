using System;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Tests.Posts
{
    [TestFixture]
    public class PublishPostTests
    {
        private Post _post;
        private PostPublished _event;

        [SetUp]
        public void Setup()
        {
            _post = PostFactories.Post();
            _post.Publish();
            _event = _post.Events.OfType<PostPublished>().Single();
        }

        [Test]
        public void ThrowsApplicationExceptionWhenAlreadyPublished()
        {
            Assert.Throws<ApplicationException>(() => _post.Publish());
        }

        [Test]
        public void ThrowsApplicationExceptionWhenDeleted()
        {
            _post.Delete();
            Assert.Throws<ApplicationException>(() => _post.Publish());
        }

        [Test]
        public void ThrowsApplicationExceptionWhenContentIsEmpty()
        {
            var command = PostFactories.UpdatePostCommand();
            command.Content = string.Empty;
            command.Status = PostStatus.Draft;
            var validatorMock = new Mock<IValidator<UpdatePost>>();
            validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            _post.Update(command, validatorMock.Object);
            Assert.Throws<ApplicationException>(() => _post.Publish());
        }

        [Test]
        public void SetsStatus()
        {
            Assert.AreEqual(PostStatus.Published, _post.Status);
        }

        [Test]
        public void SetsStatusTimeStamp()
        {
            Assert.AreEqual(_event.TimeStamp, _post.StatusTimeStamp);
        }
    }
}
