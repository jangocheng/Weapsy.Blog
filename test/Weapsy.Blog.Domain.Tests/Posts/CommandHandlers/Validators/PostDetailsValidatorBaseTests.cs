using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Posts;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators;
using Weapsy.Blog.Domain.Posts.Rules;

namespace Weapsy.Blog.Domain.Tests.Posts.CommandHandlers.Validators
{
    [TestFixture]
    public class PostDetailsValidatorBaseTests
    {
        [Test]
        public void ShouldHaveValidationErrorWhenTitleIsEmpty()
        {
            var command = PostFactories.CreatePostCommand();
            command.Title = string.Empty;
            var postRulesMock = new Mock<IPostRules>();
            postRulesMock.Setup(x => x.IsTitleUniqueAsync(command.BlogId, command.AggregateRootId, command.Title)).ReturnsAsync(true);
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenTitleIsTooLong()
        {
            var command = PostFactories.CreatePostCommand();
            command.Title = SharedFactories.RandomString(101);
            var postRulesMock = new Mock<IPostRules>();
            postRulesMock.Setup(x => x.IsTitleUniqueAsync(command.BlogId, command.AggregateRootId, command.Title)).ReturnsAsync(true);
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenTitleIsNotUnique()
        {
            var command = PostFactories.CreatePostCommand();
            var postRulesMock = new Mock<IPostRules>();
            postRulesMock.Setup(x => x.IsTitleUniqueAsync(command.BlogId, command.AggregateRootId, command.Title)).ReturnsAsync(false);
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenSlugIsEmpty()
        {
            var command = PostFactories.CreatePostCommand();
            command.Slug = string.Empty;
            var postRulesMock = new Mock<IPostRules>();
            postRulesMock.Setup(x => x.IsSlugUniqueAsync(command.BlogId, command.AggregateRootId, command.Slug)).ReturnsAsync(true);
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Slug, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenSlugIsTooLong()
        {
            var command = PostFactories.CreatePostCommand();
            command.Slug = SharedFactories.RandomString(101);
            var postRulesMock = new Mock<IPostRules>();
            postRulesMock.Setup(x => x.IsSlugUniqueAsync(command.BlogId, command.AggregateRootId, command.Slug)).ReturnsAsync(true);
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Slug, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenSlugIsNotUnique()
        {
            var command = PostFactories.CreatePostCommand();
            var postRulesMock = new Mock<IPostRules>();
            postRulesMock.Setup(x => x.IsSlugUniqueAsync(command.BlogId, command.AggregateRootId, command.Slug)).ReturnsAsync(false);
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Slug, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenExcerptIsTooLong()
        {
            var command = PostFactories.CreatePostCommand();
            command.Excerpt = SharedFactories.RandomString(251);
            var postRulesMock = new Mock<IPostRules>();
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Excerpt, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenContentIsEmptyAndStatusIsPublished()
        {
            var command = PostFactories.CreatePostCommand();
            command.Content = string.Empty;
            command.Status = PostStatus.Published;
            var postRulesMock = new Mock<IPostRules>();
            var blogRulesMock = new Mock<IBlogRules>();
            var validator = new CreatePostValidator(postRulesMock.Object, blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Content, command);
        }
    }
}
