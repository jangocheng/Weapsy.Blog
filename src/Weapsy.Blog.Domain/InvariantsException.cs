using FluentValidation.Results;

namespace Weapsy.Blog.Domain
{
    public class InvariantsException : DomainException
    {
        public InvariantsException(ValidationResult validationResult)
            : base(validationResult)
        {
        }
    }
}