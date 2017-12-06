using System;
using System.Collections.Generic;
using FluentValidation;
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
        public PostStatus Status { get; private set; }
        public DateTime StatusTimeStamp { get; private set; }
        
        public Post()
        {
        }

        public Post(CreatePost command, IValidator<CreatePost> validator) : base(command.AggregateRootId)
        {
            validator.Validate(command);
            AddEvent(command.ToEvent());
        }

        public void Update(UpdatePost command, IValidator<UpdatePost> validator)
        {
            validator.Validate(command);
            AddEvent(command.ToEvent());
        }

        public void Publish()
        {
            switch (Status)
            {
                case PostStatus.Published:
                    throw new ApplicationException("Post is already published.");
                case PostStatus.Deleted:
                    throw new ApplicationException("Post is deleted.");
            }

            if (string.IsNullOrWhiteSpace(Content))
                throw new ApplicationException("Content is required.");

            AddEvent(new PostPublished { AggregateRootId = Id });
        }

        public void Withdraw()
        {
            switch (Status)
            {
                case PostStatus.Draft:
                    throw new ApplicationException("Post is not published.");
                case PostStatus.Deleted:
                    throw new ApplicationException("Post is deleted.");
            }

            AddEvent(new PostWithdrew { AggregateRootId = Id });
        }

        public void Delete()
        {
            if (Status == PostStatus.Deleted)
                throw new ApplicationException("Post is already deleted.");

            AddEvent(new PostDeleted { AggregateRootId = Id });
        }

        public void Restore()
        {
            if (Status != PostStatus.Deleted)
                throw new ApplicationException("Post is not deleted.");

            AddEvent(new PostRestored { AggregateRootId = Id });
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
