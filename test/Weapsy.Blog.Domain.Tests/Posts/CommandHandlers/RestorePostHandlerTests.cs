﻿using System;
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
    public class RestorePostHandlerTests
    {
        private RestorePost _command;
        private Post _post;
        private Post _updatedPost;
        private IAggregateRoot _result;

        private Mock<IPostRepository> _repositoryMock;
        private Mock<IRestorePostValidator> _validatorMock;
        private IDomainCommandHandlerAsync<RestorePost> _commandHandler;

        [SetUp]
        public async Task Setup()
        {
            _post = PostFactories.DeletedPost();

            _command = new RestorePost
            {
                BlogId = Guid.NewGuid(),
                AggregateRootId = Guid.NewGuid()
            };

            _repositoryMock = new Mock<IPostRepository>();
            _repositoryMock
                .Setup(x => x.GetByIdAsync(_command.BlogId, _command.AggregateRootId))
                .ReturnsAsync(_post);
            _repositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Post>()))
                .Callback<Post>(p => _updatedPost = p)
                .Returns(Task.CompletedTask);

            _validatorMock = new Mock<IRestorePostValidator>();
            _validatorMock.Setup(x => x.Validate(_post)).Returns(new ValidationResult());

            _commandHandler = new RestorePostHandler(_repositoryMock.Object, _validatorMock.Object);

            _result = await _commandHandler.HandleAsync(_command);
        }

        [Test]
        public void ThrowsExceptionWhenPostIsNotFound()
        {
            _repositoryMock
                .Setup(x => x.GetByIdAsync(_command.BlogId, _command.AggregateRootId))
                .ReturnsAsync((Post)null);

            Assert.ThrowsAsync<ApplicationException>(async () => await _commandHandler.HandleAsync(_command));
        }

        [Test]
        public void ReturnsPost()
        {
            Assert.AreEqual(_updatedPost, _result);
        }
    }
}
