using System;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain
{
    public abstract class CommandBase : DomainCommand
    {
        public Guid BlogId { get; set; }
    }
}
