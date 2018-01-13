using FluentValidation;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions
{
    public interface IRestorePostValidator : IValidator<Post>
    {
    }
}