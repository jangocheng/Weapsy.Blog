using System.Threading.Tasks;
using Weapsy.Mediator.Queries;

namespace Weapsy.Blog.Reporting.Posts.Queries
{
    public class GetPostHandler : IQueryHandlerAsync<GetPost, PostViewModel>
    {
        public Task<PostViewModel> RetrieveAsync(GetPost query)
        {
            throw new System.NotImplementedException();
        }
    }
}
