using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators
{
    public class WithdrawPostValidator : AbstractInvariantsValidator, IWithdrawPostValidator
    {
        protected WithdrawPostValidator()
        {
            ValidateWithdraw();
        }
    }
}
