using System;
using FluentValidation;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain
{
    public static class ValidatorExtensions
    {
        public static void ValidateCommand<TCommand>(this IValidator<TCommand> validator, TCommand command) where TCommand : IDomainCommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var validationResult = validator.Validate(command);

            if (!validationResult.IsValid)
                throw new CommandException(validationResult);
        }

        public static void ValidateInvariants<TAggregateRoot>(this IValidator<TAggregateRoot> validator, TAggregateRoot aggregateRoot) where TAggregateRoot : IAggregateRoot
        {
            if (aggregateRoot == null)
                throw new ArgumentNullException(nameof(aggregateRoot));

            var validationResult = validator.Validate(aggregateRoot);

            if (!validationResult.IsValid)
                throw new InvariantsException(validationResult);
        }
    }
}
