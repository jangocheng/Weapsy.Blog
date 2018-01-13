using System.Threading.Tasks;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.CommandHandlers;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Tests.Posts.CommandHandlers
{
    [TestFixture]
    public class CreatePostHandlerTests
    {
        private CreatePost _command;
        private Post _post;
        private IAggregateRoot _result;

        private Mock<IPostRepository> _repositoryMock;
        private Mock<ICreatePostValidator> _validatorMock;
        private IDomainCommandHandlerAsync<CreatePost> _commandHandler;

        [SetUp]
        public async Task Setup()
        {
            _command = PostFactories.CreatePostCommand();

            _repositoryMock = new Mock<IPostRepository>();
            _repositoryMock
                .Setup(x => x.CreateAsync(It.IsAny<Post>()))
                .Callback<Post>(p => _post = p)
                .Returns(Task.CompletedTask);

            _validatorMock = new Mock<ICreatePostValidator>();
            _validatorMock.Setup(x => x.Validate(_command)).Returns(new ValidationResult());

            _commandHandler = new CreatePostHandler(_repositoryMock.Object, _validatorMock.Object);

            _result = await _commandHandler.HandleAsync(_command);
        }

        [Test]
        public void SavesPost()
        {
            _repositoryMock.Verify(x => x.CreateAsync(_post), Times.Once);
        }

        [Test]
        public void ReturnsPost()
        {
            Assert.AreEqual(_post, _result);
        }
    }
}
