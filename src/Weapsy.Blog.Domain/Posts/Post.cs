using System;
using System.Collections.Generic;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Events;
using Weapsy.Mediator.Domain;

namespace Weapsy.Blog.Domain.Posts
{
    public class Post : AggregateRoot
    {
        public Guid BlogId { get; private set; }
        public string Title { get; private set; }
        public string Slug { get; private set; }
        public string Excerpt { get; private set; }
        public string Content { get; private set; }
        public IEnumerable<string> Tags { get; private set; }
        public PostType Type { get; private set; }
        public PostStatus Status { get; private set; }
        public DateTime StatusTimeStamp { get; private set; }

        public Post()
        {
        }

        public Post(CreatePost command, ICreatePostValidator validator) : base(command.AggregateRootId)
        {
            validator.ValidateCommand(command);
            AddEvent(command.ToEvent());
        }

        public void Update(UpdatePost command, IUpdatePostValidator validator)
        {
            validator.ValidateCommand(command);
            AddEvent(command.ToEvent());
        }

        public void Publish(PublishPost command, IPublishPostValidator validator)
        {
            validator.ValidateInvariants(this);
            AddEvent(command.ToEvent());
        }

        public void Withdraw(WithdrawPost command, IWithdrawPostValidator validator)
        {
            validator.ValidateInvariants(this);
            AddEvent(command.ToEvent());
        }

        public void Delete(DeletePost command, IDeletePostValidator validator)
        {
            validator.ValidateInvariants(this);
            AddEvent(command.ToEvent());
        }

        public void Restore(RestorePost command, IRestorePostValidator validator)
        {
            validator.ValidateInvariants(this);
            AddEvent(command.ToEvent());
        }

        private void Apply(PostCreated @event)
        {
            Id = @event.AggregateRootId;
            BlogId = @event.BlogId;
            Title = @event.Title;
            Slug = @event.Slug;
            Excerpt = @event.Excerpt;
            Content = @event.Content;
            Tags = @event.Tags;
            Type = @event.Type;
            Status = @event.Status;
            StatusTimeStamp = @event.TimeStamp;
        }

        private void Apply(PostUpdated @event)
        {
            Title = @event.Title;
            Slug = @event.Slug;
            Excerpt = @event.Excerpt;
            Content = @event.Content;
            Tags = @event.Tags;
            Type = @event.Type;
            Status = @event.Status;           
            StatusTimeStamp = @event.TimeStamp;
        }

        private void Apply(PostPublished @event)
        {
            Status = PostStatus.Published;
            StatusTimeStamp = @event.TimeStamp;
        }

        private void Apply(PostWithdrew @event)
        {
            Status = PostStatus.Draft;
            StatusTimeStamp = @event.TimeStamp;
        }

        private void Apply(PostDeleted @event)
        {
            Status = PostStatus.Deleted;
            StatusTimeStamp = @event.TimeStamp;
        }

        private void Apply(PostRestored @event)
        {
            Status = PostStatus.Draft;
            StatusTimeStamp = @event.TimeStamp;
        }
    }
}
