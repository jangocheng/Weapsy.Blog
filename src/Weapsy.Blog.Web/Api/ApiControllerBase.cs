using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Weapsy.Mediator;

namespace Weapsy.Blog.Web.Api
{
    [Authorize(Roles = Constants.AdministratorRoleName)]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : Controllers.ControllerBase
    {
        protected ApiControllerBase(IMediator mediator) : base(mediator)
        {
        }
    }
}