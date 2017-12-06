using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
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
    public class CreatePostHandlerTests
    {
        private CreatePost _command;
        private PostCreated _event;
        private Post _post;
        private IEnumerable<IDomainEvent> _result;

        private Mock<IPostRepository> _postRepositoryMock;
        private Mock<IValidator<CreatePost>> _validatorMock;
        private IDomainCommandHandlerAsync<CreatePost> _commandHandler;

        [SetUp]
        public async Task Setup()
        {
            _command = PostFactories.CreatePostCommand();

            _validatorMock = new Mock<IValidator<CreatePost>>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());

            _postRepositoryMock = new Mock<IPostRepository>();
            _postRepositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Post>()))
                .Callback<Post>(p => _post = p)
                .Returns(Task.CompletedTask);

            _commandHandler = new CreatePostHandler(_postRepositoryMock.Object, _validatorMock.Object);
            _result =  await _commandHandler.HandleAsync(_command);

            _event = _post.Events.OfType<PostCreated>().Single();
        }

        [Test]
        public void SavesPost()
        {
            _postRepositoryMock.Verify(x => x.CreateAsync(_post), Times.Once);
        }

        [Test]
        public void ReturnsEvents()
        {
            Assert.AreEqual(_event, _result.Single());
        }
    }
}
