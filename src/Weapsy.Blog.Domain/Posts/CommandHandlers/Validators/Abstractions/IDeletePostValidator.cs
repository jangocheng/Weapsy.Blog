using FluentValidation;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions
{
    public interface IDeletePostValidator : IValidator<Post>
    {
    }
}