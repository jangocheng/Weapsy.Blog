using FluentValidation.Results;

namespace Weapsy.Blog.Domain
{
    public class CommandException : DomainException
    {
        public CommandException(ValidationResult validationResult)
            : base(validationResult)
        {
        }
    }
}