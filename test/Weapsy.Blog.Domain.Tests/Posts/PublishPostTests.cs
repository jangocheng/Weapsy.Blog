using System.Linq;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Tests.Posts
{
    [TestFixture]
    public class PublishPostTests
    {
        private PublishPost _command;
        private Mock<IPublishPostValidator> _validatorMock;
        private Post _post;
        private PostPublished _event;

        [SetUp]
        public void Setup()
        {
            _post = PostFactories.Post();
            _command = PostFactories.PublishPostCommand();
            _validatorMock = new Mock<IPublishPostValidator>();
            _validatorMock.Setup(x => x.Validate(_post)).Returns(new ValidationResult());
            _post.Publish(_command, _validatorMock.Object);
            _event = _post.Events.OfType<PostPublished>().Single();
        }

        [Test]
        public void ValidatesInvariants()
        {
            _validatorMock.Verify(x => x.Validate(_post), Times.Once);
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
