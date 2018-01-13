using FluentValidation;
using Weapsy.Blog.Domain.Posts.Commands;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions
{
    public interface IUpdatePostValidator : IValidator<UpdatePost>
    {
    }
}