using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators
{
    public class DeletePostValidator : AbstractInvariantsValidator, IDeletePostValidator
    {
        protected DeletePostValidator()
        {
            ValidateDelete();
        }
    }
}
