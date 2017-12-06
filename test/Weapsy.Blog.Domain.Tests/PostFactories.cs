using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;

namespace Weapsy.Blog.Domain.Tests
{
    public static class PostFactories
    {
        public static CreatePost CreatePostCommand()
        {
            return new CreatePost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Guid.NewGuid(),
                Content = "Content",
                Slug = "my-post",
                Excerpt = "Summary",
                Status = PostStatus.Draft,
                Tags = new List<string> { "weapsy", "blog" },
                Title = "My Post"
            };
        }

        public static PostCreated PostCreatedEvent()
        {
            return CreatePostCommand().ToEvent();
        }

        public static UpdatePost UpdatePostCommand()
        {
            return new UpdatePost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Guid.NewGuid(),
                Content = "Content updated",
                Slug = "my-post-updated",
                Excerpt = "Summary updated",
                Status = PostStatus.Published,
                Tags = new List<string> { "weapsy", "blog", "update" },
                Title = "My Post Updated"
            };
        }

        public static PostUpdated PostUpdatedEvent()
        {
            return UpdatePostCommand().ToEvent();
        }

        public static Post Post()
        {
            var command = CreatePostCommand();
            var validatorMock = new Mock<IValidator<CreatePost>>();
            validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            return new Post(command, validatorMock.Object);
        }
    }
}
