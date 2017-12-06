using Weapsy.Blog.Domain.Blogs.Rules;
using Weapsy.Blog.Domain.Posts.Commands;
using Weapsy.Blog.Domain.Posts.Rules;

namespace Weapsy.Blog.Domain.Posts.CommandHandlers.Validators
{
    public class CreatePostValidator : PostDetailsValidatorBase<CreatePost>
    {
        public CreatePostValidator(IPostRules postRules, IBlogRules blogRules) : base(postRules, blogRules)
        {
            ValidateBlogId();
            ValidateTitle();
            ValidateSlug();
            ValidateExcerpt();
            ValidateContent();
        }
    }
}
