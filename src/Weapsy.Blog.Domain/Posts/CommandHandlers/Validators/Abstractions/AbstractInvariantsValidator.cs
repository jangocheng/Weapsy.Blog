using FluentValidation;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions
{
    public class AbstractInvariantsValidator : AbstractValidator<Post>
    {
        protected void ValidateContent()
        {
            RuleFor(c => c.Content)
                .NotEmpty().WithMessage("Content is required.");
        }

        protected void ValidatePublish()
        {
            RuleFor(c => c.Status)
                .Must(NotBePublished).WithMessage("Post is already published.")
                .Must(NotBeDeleted).WithMessage("Post is deleted.");
        }

        protected void ValidateWithdraw()
        {
            RuleFor(c => c.Status)
                .Must(BePublished).WithMessage("Post is not published.")
                .Must(NotBeDeleted).WithMessage("Post is deleted.");
        }

        protected void ValidateDelete()
        {
            RuleFor(c => c.Status)
                .Must(NotBeDeleted).WithMessage("Post is already deleted.");
        }

        protected void ValidateRestore()
        {
            RuleFor(c => c.Status)
                .Must(BeDeleted).WithMessage("Post is not deleted.");
        }

        private static bool NotBePublished(PostStatus status)
        {
            return status != PostStatus.Published;
        }

        private static bool BePublished(PostStatus status)
        {
            return status == PostStatus.Published;
        }

        private static bool NotBeDeleted(PostStatus status)
        {
            return status != PostStatus.Deleted;
        }

        private static bool BeDeleted(PostStatus status)
        {
            return status == PostStatus.Deleted;
        }
    }
}
