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
    public class CreatePostTests
    {
        private CreatePost _command;
        private Mock<IValidator<CreatePost>> _validatorMock;
        private Post _post;
        private PostCreated _event;

        [SetUp]
        public void Setup()
        {
            _command = PostFactories.CreatePostCommand();
            _validatorMock = new Mock<IValidator<CreatePost>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());
            _post = new Post(_command, _validatorMock.Object);
            _event = _post.Events.OfType<PostCreated>().Single();
        }

        [Test]
        public void ValidatesCommand()
        {
            _validatorMock.Verify(x => x.Validate(_command), Times.Once);
        }

        [Test]
        public void SetsId()
        {
            Assert.AreEqual(_event.AggregateRootId, _post.Id);
        }

        [Test]
        public void SetsTitle()
        {
            Assert.AreEqual(_event.Title, _post.Title);
        }

        [Test]
        public void SetsSlug()
        {
            Assert.AreEqual(_event.Slug, _post.Slug);
        }

        [Test]
        public void SetsExcerpt()
        {
            Assert.AreEqual(_event.Excerpt, _post.Excerpt);
        }

        [Test]
        public void SetsContent()
        {
            Assert.AreEqual(_event.Content, _post.Content);
        }

        [Test]
        public void SetsType()
        {
            Assert.AreEqual(_event.Type, _post.Type);
        }

        [Test]
        public void SetsStatus()
        {
            Assert.AreEqual(_event.Status, _post.Status);
        }

        [Test]
        public void SetsStatusTimeStamp()
        {
            Assert.AreEqual(_event.TimeStamp, _post.StatusTimeStamp);
        }

        [Test]
        public void SetsTags()
        {
            Assert.AreEqual(_event.Tags, _post.Tags);
        }
    }
}
