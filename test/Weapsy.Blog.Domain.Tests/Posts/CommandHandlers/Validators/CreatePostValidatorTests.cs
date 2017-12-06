using System;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators;
using Weapsy.Blog.Domain.Posts.Rules;

namespace Weapsy.Blog.Domain.Tests.Posts.CommandHandlers.Validators
{
    [TestFixture]
    public class CreatePostValidatorTests
    {
        [Test]
        public void ShouldHaveValidationErrorWhenBlogIdIsEmpty()
        {
            var command = PostFactories.CreatePostCommand();
            command.BlogId = Guid.Empty;
            var postRulesMock = new Mock<IPostRules>();
            var blogRulesMock = new Mock<IBlogRules>();
            blogRulesMock.Setup(x => x.DoesBlogExistAsync(command.BlogId)).ReturnsAsync(true);
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenBlogDoesNotExist()
        {
            var command = PostFactories.CreatePostCommand();
            var postRulesMock = new Mock<IPostRules>();
            var blogRulesMock = new Mock<IBlogRules>();
            blogRulesMock.Setup(x => x.DoesBlogExistAsync(command.BlogId)).ReturnsAsync(false);
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }
    }
}
