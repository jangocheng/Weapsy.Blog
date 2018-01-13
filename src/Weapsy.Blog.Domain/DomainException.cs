using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Weapsy.Blog.Domain
{
    public abstract class DomainException : ApplicationException
    {
        public IEnumerable<ValidationFailure> Errors { get; }

        protected DomainException(ValidationResult validationResult)
            : base(BuildErrorMesage(validationResult.Errors))
        {
            this.Errors = validationResult.Errors;
        }

        private static string BuildErrorMesage(IEnumerable<ValidationFailure> errors)
        {
            var errorsText = errors.Select(x => "\r\n - " + x.ErrorMessage).ToArray();
            return "Validation failed: " + string.Join("", errorsText);
        }
    }
}