using System;
using Microsoft.AspNetCore.Identity;

namespace Weapsy.Blog.Data.Entities
{
    public class UserTokenEntity : IdentityUserToken<Guid>
    {
    }
}
