using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.CommandHandlers;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Tests.Posts.CommandHandlers
{
    [TestFixture]
    public class RestorePostHandlerTests
    {
        private RestorePost _command;
        private PostRestored _event;
        private Post _post;
        private Post _updatedPost;
        private IEnumerable<IDomainEvent> _result;

        private Mock<IPostRepository> _postRepositoryMock;
        private IDomainCommandHandlerAsync<RestorePost> _commandHandler;

        [SetUp]
        public async Task Setup()
        {
            _post = PostFactories.Post();
            _post.Delete();

            _command = new RestorePost
            {
                BlogId = Guid.NewGuid(),
                AggregateRootId = Guid.NewGuid()
            };

            _postRepositoryMock = new Mock<IPostRepository>();
            _postRepositoryMock
                .Setup(x => x.GetByIdAsync(_command.BlogId, _command.AggregateRootId))
                .ReturnsAsync(_post);
            _postRepositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Post>()))
                .Callback<Post>(p => _updatedPost = p)
                .Returns(Task.CompletedTask);

            _commandHandler = new RestorePostHandler(_postRepositoryMock.Object);
            _result = await _commandHandler.HandleAsync(_command);

            _event = _updatedPost.Events.OfType<PostRestored>().Single();
        }

        [Test]
        public void ThrowsExceptionWhenPostIsNotFound()
        {
            _postRepositoryMock
                .Setup(x => x.GetByIdAsync(_command.BlogId, _command.AggregateRootId))
                .ReturnsAsync((Post)null);

            Assert.ThrowsAsync<ApplicationException>(async () => await _commandHandler.HandleAsync(_command));
        }

        [Test]
        public void ReturnsEvents()
        {
            Assert.AreEqual(_event, _result.OfType<PostRestored>().Single());
        }
    }
}
