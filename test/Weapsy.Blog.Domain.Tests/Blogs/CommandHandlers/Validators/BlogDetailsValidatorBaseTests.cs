using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using Weapsy.Blog.Domain.Blogs.CommandHandlers.Validators;
using Weapsy.Blog.Domain.Blogs.Rules;

namespace Weapsy.Blog.Domain.Tests.Blogs.CommandHandlers.Validators
{
    [TestFixture]
    public class BlogDetailsValidatorBaseTests
    {
        [Test]
        public void ShouldHaveValidationErrorWhenTitleIsEmpty()
        {
            var command = BlogFactories.CreateBlogCommand();
            command.Title = string.Empty;
            var blogRulesMock = new Mock<IBlogRules>();
            blogRulesMock.Setup(x => x.IsTitleUniqueAsync(command.Title)).ReturnsAsync(true);
            var validator = new CreateBlogValidator(blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenTitleIsTooLong()
        {
            var command = BlogFactories.CreateBlogCommand();
            command.Title = SharedFactories.RandomString(101);
            var blogRulesMock = new Mock<IBlogRules>();
            blogRulesMock.Setup(x => x.IsTitleUniqueAsync(command.Title)).ReturnsAsync(true);
            var validator = new CreateBlogValidator(blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }

        [Test]
        public void ShouldHaveValidationErrorWhenTitleIsNotUnique()
        {
            var command = BlogFactories.CreateBlogCommand();
            var blogRulesMock = new Mock<IBlogRules>();
            blogRulesMock.Setup(x => x.IsTitleUniqueAsync(command.Title)).ReturnsAsync(false);
            var validator = new CreateBlogValidator(blogRulesMock.Object);
            validator.ShouldHaveValidationErrorFor(x => x.Title, command);
        }
    }
}
