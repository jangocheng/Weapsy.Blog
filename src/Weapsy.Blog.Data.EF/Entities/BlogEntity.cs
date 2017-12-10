using System;

namespace Weapsy.Blog.Data.EF.Entities
{
    public class BlogEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Theme { get; set; }
    }
}
