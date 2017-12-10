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
    public class UpdatePostTests
    {
        private UpdatePost _command;
        private Mock<IValidator<UpdatePost>> _validatorMock;
        private Post _post;
        private PostUpdated _event;

        [SetUp]
        public void Setup()
        {
            _post = PostFactories.Post();
            _command = PostFactories.UpdatePostCommand();
            _validatorMock = new Mock<IValidator<UpdatePost>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());
            _post.Update(_command, _validatorMock.Object);
            _event = _post.Events.OfType<PostUpdated>().Single();
        }

        [Test]
        public void ValidatesCommand()
        {
            _validatorMock.Verify(x => x.Validate(_command), Times.Once);
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
