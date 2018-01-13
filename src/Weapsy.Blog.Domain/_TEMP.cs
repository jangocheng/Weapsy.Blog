using FluentValidation;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain
{
    public interface ICommandContext<TCommand> where TCommand : IDomainCommand
    {
        void Execute(IAggregateRoot aggregateRoot, TCommand command);
    }

    public class CommandContext<TCommand> : ICommandContext<TCommand> where TCommand : IDomainCommand
    {
        private readonly IValidator<TCommand> _validator;

        public CommandContext(IValidator<TCommand> validator)
        {
            _validator = validator;
        }

        public void Execute(IAggregateRoot aggregateRoot, TCommand command)
        {
            //_validator.Validate();
        }
    }

    public interface ICommandValidator
    {
        
    }
}
