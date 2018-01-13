using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators
{
    public class PublishPostValidator : AbstractInvariantsValidator, IPublishPostValidator
    {
        protected PublishPostValidator()
        {
            ValidatePublish();
            ValidateContent();
        }
    }
}
