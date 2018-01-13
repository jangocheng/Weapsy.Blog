using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators
{
    public class RestorePostValidator : AbstractInvariantsValidator, IRestorePostValidator
    {
        protected RestorePostValidator()
        {
            ValidateRestore();
        }
    }
}
