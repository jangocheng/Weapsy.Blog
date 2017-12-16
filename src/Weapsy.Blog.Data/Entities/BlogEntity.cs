using System;
using System.Collections.Generic;

namespace Weapsy.Blog.Data.Entities
{
    public class BlogEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }

        public virtual IList<PostEntity> Posts { get; set; }
    }
}
