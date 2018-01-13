using System;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain
{
    public abstract class EventBase : DomainEvent
    {
        public Guid BlogId { get; set; }
    }
}
