using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Rules;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions
{
    public abstract class AbstractCommandValidator<T> : AbstractValidator<T> where T : PostDetailsBase
    {
        private readonly IPostRules _postRules;
        private readonly IBlogRules _blogRules;

        protected AbstractCommandValidator(IPostRules postRules, IBlogRules blogRules)
        {
            _postRules = postRules;
            _blogRules = blogRules;
        }

        protected void ValidateBlogId()
        {
            RuleFor(c => c.BlogId)
                .NotEmpty().WithMessage("Blog id is required.")
                .MustAsync(BeAnExistingBlog).WithMessage("Blog does not exist.");
        }

        protected void ValidateTitle()
        {
            RuleFor(c => c.Title)
                .NotEmpty().WithMessage("Title is required.")
                .Length(1, 100).WithMessage("Title max length is 100 characters.")
                .MustAsync(HaveUniqueTitle).WithMessage("A post with the same title already exists.");
        }

        protected void ValidateSlug()
        {
            RuleFor(c => c.Slug)
                .NotEmpty().WithMessage("Slug is required.")
                .Length(1, 100).WithMessage("Slug max length is 100 characters.")
                .MustAsync(HaveUniqueSlug).WithMessage("A post with the same title already exists.");
        }

        protected void ValidateExcerpt()
        {
            RuleFor(c => c.Excerpt)
                .Length(1, 250).WithMessage("Excerpt max length is 250 characters.")
                .When(c => !string.IsNullOrEmpty(c.Excerpt));
        }

        protected void ValidateContent()
        {
            RuleFor(c => c.Content)
                .NotEmpty().WithMessage("Content is required.")
                .When(c => c.Status == PostStatus.Published);
        }

        private async Task<bool> BeAnExistingBlog(Guid blogId, CancellationToken cancellationToken)
        {
            return await _blogRules.DoesBlogExistAsync(blogId);
        }

        private async Task<bool> HaveUniqueTitle(PostDetailsBase command, string title, CancellationToken cancellationToken)
        {
            return await _postRules.IsTitleUniqueAsync(command.BlogId, title);
        }

        private async Task<bool> HaveUniqueSlug(PostDetailsBase command, string slug, CancellationToken cancellationToken)
        {
            return await _postRules.IsSlugUniqueAsync(command.BlogId, slug);
        }
    }
}
