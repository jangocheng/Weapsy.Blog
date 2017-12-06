using FluentValidation;
using Weapsy.Blog.Domain.Blogs.Commands;
using Weapsy.Blog.Domain.Blogs.Events;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Blogs
{
    public class Blog : AggregateRoot
    {
        public string Title { get; private set; }

        public Blog()
        {
        }

        public Blog(CreateBlog command, IValidator<CreateBlog> validator) : base(command.AggregateRootId)
        {
            validator.Validate(command);

            Events.Add(command.ToEvent());
            Apply(command.ToEvent());

            //AddEvent(command.ToEvent());
        }

        public void Update(UpdateBlog command, IValidator<UpdateBlog> validator)
        {
            validator.Validate(command);
            AddEvent(command.ToEvent());
        }

        private void Apply(BlogCreated @event)
        {
            Id = @event.AggregateRootId;
            Title = @event.Title;
        }

        private void Apply(BlogUpdated @event)
        {
            Title = @event.Title;
        }
    }
}
