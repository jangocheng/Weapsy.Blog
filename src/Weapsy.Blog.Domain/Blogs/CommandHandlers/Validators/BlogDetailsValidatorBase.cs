using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Rules;

namespace Weapsy.Blog.Domain.Blogs.CommandHandlers.Validators
{
    public abstract class BlogDetailsValidatorBase<T> : AbstractValidator<T> where T : BlogDetailsBase
    {
        private readonly IBlogRules _blogRules;

        protected BlogDetailsValidatorBase(IBlogRules blogRules)
        {
            _blogRules = blogRules;
        }

        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title max length is 100 characters.")
                .MustAsync(HaveUniqueTitle).WithMessage("A blog with the same title already exists.");
        }

        private async Task<bool> HaveUniqueTitle(BlogDetailsBase command, string title, CancellationToken cancellationToken)
        {
            return await _blogRules.IsTitleUniqueAsync(title);
        }
    }
}
