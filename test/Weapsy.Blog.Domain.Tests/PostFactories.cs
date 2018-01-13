using System;
using System.Collections.Generic;
using FluentValidation.Results;
using Moq;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;

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
                Type = PostType.Article,
                Status = PostStatus.Draft,
                Tags = new List<string> { "weapsy", "blog" },
                Title = "My Post"
            };
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
                Type = PostType.Article,
                Status = PostStatus.Published,
                Tags = new List<string> { "weapsy", "blog", "update" },
                Title = "My Post Updated"
            };
        }

        public static PublishPost PublishPostCommand()
        {
            return new PublishPost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Guid.NewGuid()
            };
        }

        public static WithdrawPost WithdrawPostCommand()
        {
            return new WithdrawPost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Guid.NewGuid()
            };
        }

        public static DeletePost DeletePostCommand()
        {
            return new DeletePost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Guid.NewGuid()
            };
        }

        public static RestorePost RestorePostCommand()
        {
            return new RestorePost
            {
                AggregateRootId = Guid.NewGuid(),
                BlogId = Guid.NewGuid()
            };
        }

        public static Post Post()
        {
            var command = CreatePostCommand();
            var validatorMock = new Mock<ICreatePostValidator>();
            validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            return new Post(command, validatorMock.Object);
        }

        public static Post DeletedPost()
        {
            var post = Post();
            var command = DeletePostCommand();
            var validatorMock = new Mock<IDeletePostValidator>();
            validatorMock.Setup(x => x.Validate(post)).Returns(new ValidationResult());
            post.Delete(command, validatorMock.Object);
            return post;
        }

        public static Post PublishedPost()
        {
            var post = Post();
            var command = PublishPostCommand();
            var validatorMock = new Mock<IPublishPostValidator>();
            validatorMock.Setup(x => x.Validate(post)).Returns(new ValidationResult());
            post.Publish(command, validatorMock.Object);
            return post;
        }
    }
}
