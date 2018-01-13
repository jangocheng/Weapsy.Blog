using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Posts.CommandHandlers.Validators.Abstractions;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Rules;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators
{
    public class UpdatePostValidator : AbstractCommandValidator<UpdatePost>, IUpdatePostValidator
    {
        public UpdatePostValidator(IPostRules postRules, IBlogRules blogRules) : base(postRules, blogRules)
        {
            ValidateTitle();
            ValidateSlug();
            ValidateExcerpt();
            ValidateContent();
        }
    }
}
