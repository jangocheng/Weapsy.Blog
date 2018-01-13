using System;
using System.Collections.Generic;

namespace Weapsy.Blog.Reporting.Models
{
    public class PostViewModel
    {
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostContent { get; set; }
        public IList<string> PostTags { get; set; }
        public DateTime PostPublishedDate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
    }
}
